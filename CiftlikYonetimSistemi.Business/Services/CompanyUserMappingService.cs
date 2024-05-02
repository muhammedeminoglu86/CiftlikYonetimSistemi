using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using StackExchange.Redis;
using System.Text.Json;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.Extension;

namespace CiftlikYonetimSistemi.Business.Services
{
	public class CompanyUserMappingService : ICompanyUserMappingService
	{
		private readonly ICompanyUserMappingRepository _companyUserMappingRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		public readonly CreateMD5Hash _hashCreator;
		private readonly IUserRepository _userRepository;
		private readonly ICompanyRepository _companyRepository;

		public CompanyUserMappingService(ICompanyUserMappingRepository companyUserMappingRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, IUserRepository userRepository, ICompanyRepository company)
		{
			_companyUserMappingRepository = companyUserMappingRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_companyUserMappingRepository = companyUserMappingRepository;
			_hashCreator = createMD5Hash;
			_userRepository = userRepository;
			_companyRepository = company;
		}

		public async Task<int> AddAsync(CompanyUserMapping mapping)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _companyUserMappingRepository.AddAsync(mapping, connection, transaction);
						transaction.Commit();

						// Optional: Update Redis cache after successful insert
						var cacheKey = $"CompanyUserMapping_{id}";
						await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(mapping), TimeSpan.FromMinutes(60));

						return id;
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		public async Task UpdateAsync(CompanyUserMapping mapping)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _companyUserMappingRepository.UpdateAsync(mapping, connection, transaction);
						transaction.Commit();

						// Optional: Update Redis cache after successful update
						var cacheKey = $"CompanyUserMapping_{mapping.Id}";
						await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(mapping), TimeSpan.FromMinutes(60));

					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		public async Task DeleteAsync(int id)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _companyUserMappingRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						// Optional: Invalidate Redis cache after successful deletion
						var cacheKey = $"CompanyUserMapping_{id}";
						await _redis.KeyDeleteAsync(cacheKey);

					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		// Note: For GetAllAsync and GetOne, consider adding caching logic based on your specific use case and data sensitivity
		public async Task<IEnumerable<CompanyUserMapping>> GetAllAsync(string query, object param)
		{
			// Similar caching logic can be applied here as in HeadService.GetAllAsync method
			// For brevity, I'm omitting the cache logic, but you should include it as per your needs
			var queryHash = _hashCreator.CreateHash(query); // MD5 hash'ini oluşturuyoruz.
			var cacheKey = $"javascript_all_{queryHash}"; // Cache anahtarını oluşturuyoruz.

			try
			{
				var cachedHeads = await _redis.StringGetAsync(cacheKey);
				if (!cachedHeads.IsNullOrEmpty)
				{
					// Cache'den veriyi başarıyla çektiysek, deserialize edip dönüyoruz.
					return JsonSerializer.Deserialize<IEnumerable<CompanyUserMapping>>(cachedHeads);
				}
			}
			catch (Exception ex)
			{
				// Redis erişiminde bir hata olursa, bu hatayı loglayabiliriz.
				// Ancak, işleme devam etmek için hata fırlatmıyoruz.
				Console.WriteLine($"Redis access failed for key {cacheKey}: {ex.Message}");
			}

			// Eğer cache'de veri yoksa veya Redis erişiminde bir hata olmuşsa,
			// veritabanından verileri çekiyoruz.
			var heads = await _companyUserMappingRepository.GetAllAsync(query, param);
			foreach(var item in heads)
			{
				item.User = await _userRepository.GetOne("select * from User where id = @id and isactive = 1", new {id = item.Userid});
				item.Company = await _companyRepository.GetOne("select * from Company where id = @id and isactive = 1", new {id = item.Companyid});
			}
			try
			{
				// Veritabanından çekilen verileri cache'liyoruz.
				var expiration = TimeSpan.FromMinutes(60); // Cache süresini belirliyoruz.
				await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(heads), expiration);
			}
			catch (Exception ex)
			{
				// Redis'e veri yazarken bir hata olursa, bu hatayı loglayabiliriz.
				// Ancak, işleme devam etmek için hata fırlatmıyoruz.
				Console.WriteLine($"Failed to cache data for key {cacheKey}: {ex.Message}");
			}

			return heads; // Veritabanından çekilen verileri dönüyoruz.
		}

		public async Task<CompanyUserMapping> GetOne(string query, object param)
		{
			string cacheKey = "";
			if (!(param is { } parameters && parameters.GetType().GetProperty("Id")?.GetValue(parameters) is int id) || id <= 0)
			{
				var queryHash = _hashCreator.CreateHash(query);
				cacheKey = $"javascript_{queryHash}";
			}
			else
				cacheKey = $"javascript_{id}";

			try
			{
				var cachedHead = await _redis.StringGetAsync(cacheKey);

				if (!cachedHead.IsNullOrEmpty)
				{

					return JsonSerializer.Deserialize<CompanyUserMapping>(cachedHead);
				}
			}
			catch (Exception ex)
			{
				// Redis'e erişimde hata oluşursa, loglama yapın.
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var head = await _companyUserMappingRepository.GetOne(query, param);
			if(head != null)
			{
				head.User = await _userRepository.GetOne("select * from User where id = @id and isactive = 1", new {id = head.Userid});
				head.Company = await _companyRepository.GetOne("select * from Company where id = @id and isactive = 1", new {id = head.Companyid});
			}
			if (head != null)
			{
				try
				{
					await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(head), TimeSpan.FromMinutes(60));
				}
				catch (Exception ex)
				{
					// Cache güncellemesi sırasında hata oluşursa, loglama yapın.
					Console.WriteLine($"Redis cache update failed: {ex.Message}");
				}
			}
			return head;
		}
	}
}
