using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalSupplementMappingRepository :IAnimalSupplementMappingRepository
{
	private readonly DapperContext _context;

	public AnimalSupplementMappingRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalSupplementMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalSupplementMapping>(query, param);
		}
	}

	public async Task<AnimalSupplementMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalSupplementMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalSupplementMapping mapping)
	{
		var query = "INSERT INTO AnimalSupplementMapping (AnimalId, SupplementId, CreationDate, IsActive) VALUES (@AnimalId, @SupplementId, @CreationDate, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalSupplementMapping mapping)
	{
		var query = "UPDATE AnimalSupplementMapping SET AnimalId = @AnimalId, SupplementId = @SupplementId, CreationDate = @CreationDate, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalSupplementMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
