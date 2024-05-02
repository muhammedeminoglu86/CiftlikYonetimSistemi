using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using static Dapper.SqlMapper;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class DiseaseRepository : IDiseaseRepository
{
	private readonly DapperContext _context;

	public DiseaseRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<Disase>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Disase>(query, param);
		}
	}

	public async Task<Disase> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Disase>(query, param);
		}
	}
	public async Task<int> AddAsync(Disase disease)
	{
		var query = @"INSERT INTO Disease (DiseaseName, DiseaseDescription, IsActive) VALUES (@DiseaseName, @DiseaseDescription, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(Disase disease)
	{
		var query = "UPDATE Disease SET DiseaseName = @DiseaseName, DiseaseDescription = @DiseaseDescription, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { DiseaseName = disease.Disasename, DiseaseDescription = disease.Disasedescription, IsActive = disease.Isactive, Id = disease.Id });
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Disease WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
