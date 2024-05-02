using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class UserRepository : IUserRepository
{
	private readonly DapperContext _context;

	public UserRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<User>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryAsync<User>(query, param, transaction);
	}

	public async Task<User> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var conn = connection ?? _context.CreateConnection();
		return await conn.QueryFirstOrDefaultAsync<User>(query, param, transaction);
	}

	public async Task<int> AddAsync(User user, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"
            INSERT INTO [User] (Username, Password, Email, IsActive, UserTypeId) 
            VALUES (@Username, @Password, @Email, @IsActive, @UserTypeId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		var conn = connection ?? _context.CreateConnection();
		var id = await conn.ExecuteScalarAsync<int>(query, user, transaction);
		return id;
	}

	public async Task UpdateAsync(User user, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = @"
            UPDATE [User]
            SET Username = @Username, Password = @Password, Email = @Email, IsActive = @IsActive, UserTypeId = @UserTypeId 
            WHERE Id = @Id;
        ";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, user, transaction);
	}

	public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
	{
		var query = "DELETE FROM [User] WHERE Id = @Id";
		var conn = connection ?? _context.CreateConnection();
		await conn.ExecuteAsync(query, new { Id = id }, transaction);
	}
}
