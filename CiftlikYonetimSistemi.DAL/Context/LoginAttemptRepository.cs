using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class LoginAttemptRepository : ILoginAttemptRepository
{
	private readonly DapperContext _context;

	public LoginAttemptRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<LoginAttempt>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<LoginAttempt>(query, param);
		}
	}

	public async Task<LoginAttempt> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<LoginAttempt>(query, param);
		}
	}
	public async Task<int> AddAsync(LoginAttempt loginAttempt)
	{
		var query = @"
            INSERT INTO LoginAttempts (Username, Password, IpAddress, AttemptTime, IsSuccess) 
            VALUES (@Username, @Password, @IpAddress, @AttemptTime, @IsSuccess);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, loginAttempt);
			return id;
		}
	}

	public async Task UpdateAsync(LoginAttempt loginAttempt)
	{
		var query = @"
            UPDATE LoginAttempts 
            SET Username = @Username, Password = @Password, IpAddress = @IpAddress, 
                AttemptTime = @AttemptTime, IsSuccess = @IsSuccess 
            WHERE Id = @Id
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, loginAttempt);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM LoginAttempts WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
