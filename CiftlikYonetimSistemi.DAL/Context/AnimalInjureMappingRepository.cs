using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalInjureMappingRepository : IAnimalInjureMappingRepository
{
	private readonly DapperContext _context;

	public AnimalInjureMappingRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<AnimalInjureMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalInjureMapping>(query, param);
		}
	}

	public async Task<AnimalInjureMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalInjureMapping>(query, param);
		}
	}

	public async Task<int> AddAsync(AnimalInjureMapping mapping)
	{
		var query = "INSERT INTO AnimalInjureMapping (AnimalId, InjureId, IsActive, CreationDate, IsFinished) VALUES (@AnimalId, @InjureId, @IsActive, @CreationDate, @IsFinished);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalInjureMapping mapping)
	{
		var query = "UPDATE AnimalInjureMapping SET AnimalId = @AnimalId, InjureId = @InjureId, IsActive = @IsActive, CreationDate = @CreationDate, IsFinished = @IsFinished WHERE Id = @Id;SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteScalarAsync<int>(query);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalInjureMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	
}
