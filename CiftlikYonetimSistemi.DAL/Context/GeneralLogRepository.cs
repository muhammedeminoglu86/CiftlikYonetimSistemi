using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class GeneralLogRepository : IGeneralLogRepository
{
	private readonly DapperContext _context;

	public GeneralLogRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<GeneralLog>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<GeneralLog>(query, param);
		}
	}

	public async Task<GeneralLog> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<GeneralLog>(query, param);
		}
	}

	public async Task<int> AddAsync(GeneralLog generalLog)
	{
		var query = @"
            INSERT INTO GeneralLog (ControllerName, ActionName, Description, LogTypeId, ExceptionMessage, CompanyUserMappingId, CreationDate) 
            VALUES (@ControllerName, @ActionName, @Description, @LogTypeId, @ExceptionMessage, @CompanyUserMappingId, @CreationDate);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, generalLog);
			return id;
		}
	}

	public async Task UpdateAsync(GeneralLog generalLog)
	{
		var query = @"
            UPDATE GeneralLog 
            SET ControllerName = @ControllerName, ActionName = @ActionName, Description = @Description, 
                LogTypeId = @LogTypeId, ExceptionMessage = @ExceptionMessage, CompanyUserMappingId = @CompanyUserMappingId, CreationDate = @CreationDate
            WHERE Id = @Id
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, generalLog);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM GeneralLog WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
