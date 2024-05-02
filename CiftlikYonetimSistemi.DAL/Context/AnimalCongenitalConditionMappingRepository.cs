using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalCongenitalConditionMappingRepository : IAnimalCongenitalConditionMappingRepository
{
	private readonly DapperContext _context;

	public AnimalCongenitalConditionMappingRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<AnimalCongenitalConditionMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalCongenitalConditionMapping>(query, param);
		}
	}

	public async Task<AnimalCongenitalConditionMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalCongenitalConditionMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalCongenitalConditionMapping mapping)
	{
		var query = "INSERT INTO AnimalCongenitalConditionMapping (AnimalId, CongetialConditionId, CreationDate, IsFinished, FinishTime, IsActive) VALUES (@AnimalId, @CongetialConditionId, @CreationDate, @IsFinished, @FinishTime, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalCongenitalConditionMapping mapping)
	{
		var query = "UPDATE AnimalCongenitalConditionMapping SET AnimalId = @AnimalId, CongetialConditionId = @CongetialConditionId, CreationDate = @CreationDate, IsFinished = @IsFinished, FinishTime = @FinishTime, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalCongenitalConditionMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
