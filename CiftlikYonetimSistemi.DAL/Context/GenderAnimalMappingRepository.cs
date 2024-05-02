using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class GenderAnimalMappingRepository : IGenderAnimalMappingRepository
{
	private readonly DapperContext _context;

	public GenderAnimalMappingRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<GenderAnimalMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<GenderAnimalMapping>(query, param);
		}
	}

	public async Task<GenderAnimalMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<GenderAnimalMapping>(query, param);
		}
	}

	public async Task<int> AddAsync(GenderAnimalMapping mapping)
	{
		var query = @"
            INSERT INTO GenderAnimalMapping (AnimalId, GenderId, IsActive, CreationDate) 
            VALUES (@AnimalId, @GenderId, @IsActive, @CreationDate);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, mapping);
			return id;
		}
	}

	public async Task UpdateAsync(GenderAnimalMapping mapping)
	{
		var query = @"
            UPDATE GenderAnimalMapping 
            SET AnimalId = @AnimalId, GenderId = @GenderId, IsActive = @IsActive, CreationDate = @CreationDate 
            WHERE Id = @Id
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM GenderAnimalMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
