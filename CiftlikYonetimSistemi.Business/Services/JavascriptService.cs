using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.Extension;
using Dapper;
using StackExchange.Redis;

namespace CiftlikYonetimSistemi.Business.Services
{
    public class JavascriptService : IJavascriptService
	{
		private readonly IJavascriptRepository _javascriptRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		private readonly CreateMD5Hash _hashCreator;
		public JavascriptService(IJavascriptRepository javascriptRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash)
		{
			_javascriptRepository = javascriptRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_hashCreator = createMD5Hash;
		}

		public async Task<int> AddAsync(Javascript javascript)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _javascriptRepository.AddAsync(javascript, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"javascript_{id}";
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(javascript), TimeSpan.FromMinutes(60));
						}
						catch (Exception ex)
						{
							Console.WriteLine($"Redis cache update failed: {ex.Message}");
						}

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

		public async Task UpdateAsync(Javascript javascript)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _javascriptRepository.UpdateAsync(javascript, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"javascript_{javascript.Id}";
							await _redis.KeyDeleteAsync(cacheKey);
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(javascript), TimeSpan.FromMinutes(60));
						}
						catch (Exception ex)
						{
							Console.WriteLine($"Failed to update Redis cache: {ex.Message}");
						}
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
						await _javascriptRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"javascript_{id}";
							await _redis.KeyDeleteAsync(cacheKey);
						}
						catch (Exception ex)
						{
							Console.WriteLine($"Failed to invalidate Redis cache for deleted item: {ex.Message}");
						}
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		public async Task<IEnumerable<Javascript>> GetAllAsync(string query, object param)
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
					return JsonSerializer.Deserialize<IEnumerable<Javascript>>(cachedHeads);
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
			var heads = await _javascriptRepository.GetAllAsync(query, param);

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
			return await _javascriptRepository.GetAllAsync(query, param);
		}

		public async Task<Javascript> GetOne(string query, object param)
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

					return JsonSerializer.Deserialize<Javascript>(cachedHead);
				}
			}
			catch (Exception ex)
			{
				// Redis'e erişimde hata oluşursa, loglama yapın.
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var head = await _javascriptRepository.GetOne(query, param);
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
