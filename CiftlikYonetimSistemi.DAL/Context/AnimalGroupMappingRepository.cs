using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalGroupMappingRepository : IAnimalGroupMappingRepository
{
	private readonly DapperContext _context;

	public AnimalGroupMappingRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalGroupMapping>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalGroupMapping>(query, param);
		}
	}

	public async Task<AnimalGroupMapping> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalGroupMapping>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalGroupMapping mapping)
	{
		var query = "INSERT INTO AnimalGroupMapping (AnimalId, GroupId, IsActive, CreationDate) VALUES (@AnimalId, @GroupId, @IsActive, @CreationDate);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalGroupMapping mapping)
	{
		var query = "UPDATE AnimalGroupMapping SET AnimalId = @AnimalId, GroupId = @GroupId, IsActive = @IsActive, CreationDate = @CreationDate WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, mapping);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalGroupMapping WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
