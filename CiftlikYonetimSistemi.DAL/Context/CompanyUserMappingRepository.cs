using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class CompanyUserMappingRepository : ICompanyUserMappingRepository
{
	private readonly DapperContext _context;

	public CompanyUserMappingRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<CompanyUserMapping>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryAsync<CompanyUserMapping>(query, param, transaction);
	}

	public async Task<CompanyUserMapping> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryFirstOrDefaultAsync<CompanyUserMapping>(query, param, transaction);
	}

	public async Task<int> AddAsync(CompanyUserMapping mapping, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"INSERT INTO CompanyUserMapping (UserId, CompanyId, AssignedBy, CreationDate, IpAddress) 
                      VALUES (@UserId, @CompanyId, @AssignedBy, @CreationDate, @IpAddress);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
		var conn = connection ?? _context.CreateConnection();
		var id = await conn.ExecuteScalarAsync<int>(query, mapping, transaction);
		return id;
	}

	public async Task UpdateAsync(CompanyUserMapping mapping, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"UPDATE CompanyUserMapping 
                      SET UserId = @UserId, CompanyId = @CompanyId, AssignedBy = @AssignedBy, CreationDate = @CreationDate, IpAddress = @IpAddress 
                      WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, mapping, transaction);
	}

	public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = "DELETE FROM CompanyUserMapping WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, new { Id = id }, transaction);
	}
}
