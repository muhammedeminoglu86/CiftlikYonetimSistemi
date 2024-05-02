using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class UserLoginRepository : IUserLoginRepository
{
	private readonly DapperContext _context;

	public UserLoginRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(UserLogin userLogin, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"
            INSERT INTO UserLogin (CompanyUserMappingId, GeneratedToken, SessionTimeout, LoginTime, IsLoggedOut, LogoutTime, AttemptId) 
            VALUES (@CompanyUserMappingId, @GeneratedToken, @SessionTimeout, @LoginTime, @IsLoggedOut, @LogoutTime, @AttemptId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		var conn = connection ?? _context.CreateConnection();
		var id = await conn.ExecuteScalarAsync<int>(query, userLogin, transaction);
		return id;
	}

	public async Task UpdateAsync(UserLogin userLogin, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"
            UPDATE UserLogin 
            SET CompanyUserMappingId = @CompanyUserMappingId, GeneratedToken = @GeneratedToken, 
                SessionTimeout = @SessionTimeout, LoginTime = @LoginTime, 
                IsLoggedOut = @IsLoggedOut, LogoutTime = @LogoutTime, AttemptId = @AttemptId
            WHERE Id = @Id;
        ";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, userLogin, transaction);
	}

	public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = "DELETE FROM UserLogin WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, new { Id = id }, transaction);
	}

	public async Task<IEnumerable<UserLogin>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryAsync<UserLogin>(query, param, transaction);
	}

	public async Task<UserLogin> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryFirstOrDefaultAsync<UserLogin>(query, param, transaction);
	}
}
