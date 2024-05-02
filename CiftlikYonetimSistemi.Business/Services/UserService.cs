using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.Extension;
using StackExchange.Redis;

namespace CiftlikYonetimSistemi.Business.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		private readonly CreateMD5Hash _hashCreator;
		private readonly ICompanyUserMappingRepository _companyUserMappingRepository;

		public UserService(IUserRepository userRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, ICompanyUserMappingRepository companyUserMappingRepository)
		{
			_userRepository = userRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_hashCreator = createMD5Hash;
			_companyUserMappingRepository = companyUserMappingRepository;
		}

		public async Task<int> AddAsync(User user)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _userRepository.AddAsync(user, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"user_{id}";
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(user), TimeSpan.FromMinutes(60));
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

		public async Task UpdateAsync(User user)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _userRepository.UpdateAsync(user, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"user_{user.Id}";
							await _redis.KeyDeleteAsync(cacheKey);
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(user), TimeSpan.FromMinutes(60));
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
						await _userRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"user_{id}";
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

		public async Task<IEnumerable<User>> GetAllAsync(string query, object param)
		{
			var queryHash = _hashCreator.CreateHash(query);
			var cacheKey = $"users_all_{queryHash}";

			try
			{
				var cachedUsers = await _redis.StringGetAsync(cacheKey);
				if (!cachedUsers.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<IEnumerable<User>>(cachedUsers);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed for key {cacheKey}: {ex.Message}");
			}

			var users = await _userRepository.GetAllAsync(query, param);

			try
			{
				await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(users), TimeSpan.FromMinutes(60));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to cache data for key {cacheKey}: {ex.Message}");
			}

			return users;
		}

		public async Task<User> GetOne(string query, object param)
		{
			string cacheKey = "";
			if (!(param is { } parameters && parameters.GetType().GetProperty("Id")?.GetValue(parameters) is int id) || id <= 0)
			{
				var queryHash = _hashCreator.CreateHash(query);
				cacheKey = $"user_{queryHash}";
			}
			else
				cacheKey = $"user_{id}";

			try
			{
				var cachedUser = await _redis.StringGetAsync(cacheKey);

				if (!cachedUser.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<User>(cachedUser);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var user = await _userRepository.GetOne(query, param);
			if (user != null)
			{
				try
				{
					user.CompanyUserMappings = _companyUserMappingRepository.GetAllAsync("select * from CompanyUserMapping where userid = @id", new { id = user.Id }).Result.ToList();
					await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(user), TimeSpan.FromMinutes(60));
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Redis cache update failed: {ex.Message}");
				}
			}

			return user;
		}

		public async Task<User> ValidateLoginAsync(LoginDTO loginDTO)
		{
			// Assuming we use a basic query to retrieve the user by username
			// You would need to adjust the query to match your database schema and security practices, e.g., using parameterized queries
			string query = "SELECT * FROM User WHERE email = @email and password = @password";
			var user = await GetOne(query, new { email = loginDTO.Email, password = loginDTO.Password });
			return user;
			
		}

	}
}
