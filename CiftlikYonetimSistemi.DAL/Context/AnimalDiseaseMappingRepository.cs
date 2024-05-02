using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalDiseaseMappingRepository : IAnimalDiseaseMappingRepository
{
	private readonly DapperContext _context;

	public AnimalDiseaseMappingRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalDisaseMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalDisaseMapping>(query, param);
		}
	}

	public async Task<AnimalDisaseMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalDisaseMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalDisaseMapping mapping)
	{
		var query = "INSERT INTO AnimalDiseaseMapping (AnimalId, DiseaseId, CreationDate, IsFinished, FinishTime, IsActive) VALUES (@AnimalId, @DiseaseId, @CreationDate, @IsFinished, @FinishTime, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalDisaseMapping mapping)
	{
		var query = "UPDATE AnimalDiseaseMapping SET AnimalId = @AnimalId, DiseaseId = @DiseaseId, CreationDate = @CreationDate, IsFinished = @IsFinished, FinishTime = @FinishTime, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalDiseaseMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
