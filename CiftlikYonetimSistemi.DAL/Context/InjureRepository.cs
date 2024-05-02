using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class InjureRepository : IInjureRepository
{
	private readonly DapperContext _context;

	public InjureRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Injure>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Injure>(query, param);
		}
	}

	public async Task<Injure> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Injure>(query, param);
		}
	}

	public async Task<int> AddAsync(Injure injure)
	{
		var query = @"
            INSERT INTO Injure (InjureName, InsureDescription, IsActive) 
            VALUES (@InjureName, @InsureDescription, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, injure);
			return id;
		}
	}

	public async Task UpdateAsync(Injure injure)
	{
		var query = @"
            UPDATE Injure 
            SET InjureName = @InjureName, InsureDescription = @InsureDescription, IsActive = @IsActive 
            WHERE Id = @Id
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, injure);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Injure WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
