using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class CompanyRepository : ICompanyRepository
{
	private readonly DapperContext _context;

	public CompanyRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Company>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryAsync<Company>(query, param, transaction);
	}

	public async Task<Company> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryFirstOrDefaultAsync<Company>(query, param, transaction);
	}

	public async Task<int> AddAsync(Company company, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"INSERT INTO Company (CompanyName, CompanyDescription, CompanyLogo, Phone1, Phone2, Manager1, Manager2, Address, IsActive) 
                      VALUES (@CompanyName, @CompanyDescription, @CompanyLogo, @Phone1, @Phone2, @Manager1, @Manager2, @Address, @IsActive);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
		var conn = connection ?? _context.CreateConnection();
		var id = await conn.ExecuteScalarAsync<int>(query, company, transaction);
		return id;
	}

	public async Task UpdateAsync(Company company, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"UPDATE Company 
                      SET CompanyName = @CompanyName, CompanyDescription = @CompanyDescription, CompanyLogo = @CompanyLogo, 
                          Phone1 = @Phone1, Phone2 = @Phone2, Manager1 = @Manager1, Manager2 = @Manager2, 
                          Address = @Address, IsActive = @IsActive 
                      WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, company, transaction);
	}

	public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = "DELETE FROM Company WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, new { Id = id }, transaction);
	}
}
