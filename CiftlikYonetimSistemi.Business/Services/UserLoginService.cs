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
	public class UserLoginService : IUserLoginService
	{
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		private readonly CreateMD5Hash _hashCreator;

		public UserLoginService(IUserLoginRepository userLoginRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash)
		{
			_userLoginRepository = userLoginRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_hashCreator = createMD5Hash;
		}

		public async Task<int> AddAsync(UserLogin userLogin)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _userLoginRepository.AddAsync(userLogin, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"userLogin_{id}";
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(userLogin), TimeSpan.FromMinutes(60));
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

		public async Task UpdateAsync(UserLogin userLogin)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _userLoginRepository.UpdateAsync(userLogin, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"userLogin_{userLogin.Id}";
							await _redis.KeyDeleteAsync(cacheKey);
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(userLogin), TimeSpan.FromMinutes(60));
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
						await _userLoginRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"userLogin_{id}";
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

		public async Task<IEnumerable<UserLogin>> GetAllAsync(string query, object param)
		{
			var queryHash = _hashCreator.CreateHash(query);
			var cacheKey = $"userLogin_all_{queryHash}";

			try
			{
				var cachedLogins = await _redis.StringGetAsync(cacheKey);
				if (!cachedLogins.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<IEnumerable<UserLogin>>(cachedLogins);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed for key {cacheKey}: {ex.Message}");
			}

			var userLogins = await _userLoginRepository.GetAllAsync(query, param);

			try
			{
				await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(userLogins), TimeSpan.FromMinutes(60));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to cache data for key {cacheKey}: {ex.Message}");
			}

			return userLogins;
		}

		public async Task<UserLogin> GetOne(string query, object param)
		{
			string cacheKey = "";
			if (!(param is { } parameters && parameters.GetType().GetProperty("Id")?.GetValue(parameters) is int id) || id <= 0)
			{
				var queryHash = _hashCreator.CreateHash(query);
				cacheKey = $"userLogin_{queryHash}";
			}
			else
				cacheKey = $"userLogin_{id}";

			try
			{
				var cachedLogin = await _redis.StringGetAsync(cacheKey);

				if (!cachedLogin.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<UserLogin>(cachedLogin);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var userLogin = await _userLoginRepository.GetOne(query, param);
			if (userLogin != null)
			{
				try
				{
					await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(userLogin), TimeSpan.FromMinutes(60));
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Redis cache update failed: {ex.Message}");
				}
			}

			return userLogin;
		}
	}
}
