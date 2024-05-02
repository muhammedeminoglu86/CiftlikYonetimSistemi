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
using StackExchange.Redis;

namespace CiftlikYonetimSistemi.Business.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly DapperContext _context;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _redis;
		private readonly CreateMD5Hash _hashCreator;
		private readonly IUserRepository _userrepository;
		public CompanyService(ICompanyRepository companyRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, IUserRepository _userrepository)
		{
			_companyRepository = companyRepository;
			_context = context;
			_redisConnection = redisConnection;
			_redis = redisConnection.GetDatabase();
			_hashCreator = createMD5Hash;
			_userrepository = _userrepository;
		}

		public async Task<int> AddAsync(Company company)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var id = await _companyRepository.AddAsync(company, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"company_{id}";
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(company), TimeSpan.FromMinutes(60));
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

		public async Task UpdateAsync(Company company)
		{
			using (var connection = _context.CreateConnection())
			{
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						await _companyRepository.UpdateAsync(company, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"company_{company.Id}";
							await _redis.KeyDeleteAsync(cacheKey);
							await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(company), TimeSpan.FromMinutes(60));
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
						await _companyRepository.DeleteAsync(id, connection, transaction);
						transaction.Commit();

						try
						{
							string cacheKey = $"company_{id}";
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

		public async Task<IEnumerable<Company>> GetAllAsync(string query, object param)
		{
			var queryHash = _hashCreator.CreateHash(query);
			var cacheKey = $"companies_all_{queryHash}";

			try
			{
				var cachedCompanies = await _redis.StringGetAsync(cacheKey);
				if (!cachedCompanies.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<IEnumerable<Company>>(cachedCompanies);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed for key {cacheKey}: {ex.Message}");
			}

			var companies = await _companyRepository.GetAllAsync(query, param);
			
			try
			{
				await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(companies), TimeSpan.FromMinutes(60));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to cache data for key {cacheKey}: {ex.Message}");
			}

			return companies;
		}

		public async Task<Company> GetOne(string query, object param)
		{
			string cacheKey = "";
			if (!(param is { } parameters && parameters.GetType().GetProperty("Id")?.GetValue(parameters) is int id) || id <= 0)
			{
				var queryHash = _hashCreator.CreateHash(query);
				cacheKey = $"company_{queryHash}";
			}
			else
				cacheKey = $"company_{id}";

			try
			{
				var cachedCompany = await _redis.StringGetAsync(cacheKey);

				if (!cachedCompany.IsNullOrEmpty)
				{
					return JsonSerializer.Deserialize<Company>(cachedCompany);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Redis access failed: {ex.Message}");
			}

			var company = await _companyRepository.GetOne(query, param);
			if (company != null)
			{
				try
				{
					await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(company), TimeSpan.FromMinutes(60));
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Redis cache update failed: {ex.Message}");
				}
			}

			return company;
		}
	}
}
