using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalDrugMappingRepository : IAnimalDrugMappingRepository
{
	private readonly DapperContext _context;

	public AnimalDrugMappingRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalDrugMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalDrugMapping>(query, param);
		}
	}

	public async Task<AnimalDrugMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalDrugMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalDrugMapping mapping)
	{
		var query = "INSERT INTO AnimalDrugMapping (AnimalId, DrugId, CreationDate, IsActive, DiseaseId) VALUES (@AnimalId, @DrugId, @CreationDate, @IsActive, @DiseaseId);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalDrugMapping mapping)
	{
		var query = "UPDATE AnimalDrugMapping SET AnimalId = @AnimalId, DrugId = @DrugId, CreationDate = @CreationDate, IsActive = @IsActive, DiseaseId = @DiseaseId WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalDrugMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
