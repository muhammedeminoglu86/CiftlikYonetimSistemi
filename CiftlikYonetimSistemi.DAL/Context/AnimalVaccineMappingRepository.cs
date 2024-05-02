using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalVaccineMappingRepository : IAnimalVaccineMappingRepository
{
	private readonly DapperContext _context;

	public AnimalVaccineMappingRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalVaccineMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalVaccineMapping>(query, param);
		}
	}

	public async Task<AnimalVaccineMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalVaccineMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalVaccineMapping mapping)
	{
		var query = "INSERT INTO AnimalVaccineMapping (AnimalId, VaccineId, CreationDate, IsActive) VALUES (@AnimalId, @VaccineId, @CreationDate, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalVaccineMapping mapping)
	{
		var query = "UPDATE AnimalVaccineMapping SET AnimalId = @AnimalId, VaccineId = @VaccineId, CreationDate = @CreationDate, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalVaccineMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
