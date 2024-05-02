using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class QuantityTypeRepository : IQuantityTypeRepository
{
	private readonly DapperContext _context;

	public QuantityTypeRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(QuantityType quantityType)
	{
		var query = @"
            INSERT INTO QuantityType (QuantityTypeName, IsActive) 
            VALUES (@QuantityTypeName, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, quantityType);
			return id;
		}
	}

	public async Task UpdateAsync(QuantityType quantityType)
	{
		var query = @"
            UPDATE QuantityType 
            SET QuantityTypeName = @QuantityTypeName, IsActive = @IsActive 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, quantityType);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM QuantityType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<QuantityType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<QuantityType>(query, param);
		}
	}

	public async Task<QuantityType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<QuantityType>(query, param);
		}
	}
}
