﻿using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.Extension;
using StackExchange.Redis;
using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Services
{
	public class HeadService : IHeadService
	{
		private readonly IHeadRepository _headRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		private readonly CreateMD5Hash _hashCreator;
		private readonly IHeadValuesRepository _headValues;

		public HeadService(IHeadRepository headRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, IHeadValuesRepository headValues)
		{
			_headRepository = headRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_hashCreator = createMD5Hash;
			_headValues = headValues;

		}

		public async Task<int> AddAsync(Head head)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _headRepository.AddAsync(head, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"head_{id}";
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(head), TimeSpan.FromMinutes(60));
						}
						catch (Exception ex)
						{
							// Loglama yapabilir veya Redis ile ilgili bir hata mesajı gösterebilirsiniz.
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
		public async Task UpdateAsync(Head head)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _headRepository.UpdateAsync(head, connection, transaction);
						transaction.Commit();

						// Attempt Redis cache update
						try
						{
							string cacheKey = $"head_{head.Id}";
							await _redis.KeyDeleteAsync(cacheKey); // Invalidate existing cache
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(head), TimeSpan.FromMinutes(60)); // Add updated item to cache
						}
						catch (Exception ex)
						{
							// Log Redis error but do not throw, allowing service to continue
							Console.WriteLine($"Failed to update Redis cache: {ex.Message}");
						}
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw; // Throw DB related errors to be handled by the caller
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
						await _headRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						// Attempt Redis cache invalidation
						try
						{
							string cacheKey = $"head_{id}";
							await _redis.KeyDeleteAsync(cacheKey); // Invalidate the cache for the deleted item
						}
						catch (Exception ex)
						{
							// Log Redis error but do not throw, allowing service to continue
							Console.WriteLine($"Failed to invalidate Redis cache for deleted item: {ex.Message}");
						}
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw; // Throw DB related errors to be handled by the caller
					}
				}
			}
		}

		public async Task<IEnumerable<Head>> GetAllAsync(string query, object param)
		{
			var queryHash = _hashCreator.CreateHash(query); // MD5 hash'ini oluşturuyoruz.
			var cacheKey = $"heads_all_{queryHash}"; // Cache anahtarını oluşturuyoruz.

			try
			{
				var cachedHeads = await _redis.StringGetAsync(cacheKey);
				if (!cachedHeads.IsNullOrEmpty)
				{
					// Cache'den veriyi başarıyla çektiysek, deserialize edip dönüyoruz.
					return JsonSerializer.Deserialize<IEnumerable<Head>>(cachedHeads);
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
			var heads = await _headRepository.GetAllAsync(query, param);

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
		public async Task<Head> GetOne(string query, object param)
		{
			string cacheKey = "";
			if (!(param is { } parameters && parameters.GetType().GetProperty("Id")?.GetValue(parameters) is int id) || id <= 0)
			{
				var queryHash = _hashCreator.CreateHash(query);
				cacheKey = $"head_{queryHash}";
			}
			else
				cacheKey = $"head_{id}";

			try
			{
				var cachedHead = await _redis.StringGetAsync(cacheKey);

				if (!cachedHead.IsNullOrEmpty)
				{

					return JsonSerializer.Deserialize<Head>(cachedHead);
				}
			}
			catch (Exception ex)
			{
				// Redis'e erişimde hata oluşursa, loglama yapın.
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var head = await _headRepository.GetOne(query, param);
			if (head != null)
			{
				try
				{
					var item =  _headValues.GetAllAsync("SELECT * FROM HeadValues WHERE headid = @headid or headid = -1 order by ordernumber", new { headid = head.Id }).Result;
					head.HeadValues = item.ToList();
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
