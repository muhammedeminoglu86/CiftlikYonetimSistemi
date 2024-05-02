using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;

public class LogTypeRepository
{
	private readonly DapperContext _context;

	public LogTypeRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(LogType logType)
	{
		var query = @"
            INSERT INTO LogType (LogTypeName, LogTypeDescription, IsActive) 
            VALUES (@LogTypeName, @LogTypeDescription, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, logType);
			return id;
		}
	}
	
	public async Task UpdateAsync(LogType logType)
	{
		var query = @"
            UPDATE LogType 
            SET LogTypeName = @LogTypeName, LogTypeDescription = @LogTypeDescription, IsActive = @IsActive 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, logType);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM LogType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	// Example of retrieving all LogTypes
	public async Task<IEnumerable<LogType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<LogType>(query, param);
		}
	}

	public async Task<LogType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<LogType>(query, param);
		}
	}
}
