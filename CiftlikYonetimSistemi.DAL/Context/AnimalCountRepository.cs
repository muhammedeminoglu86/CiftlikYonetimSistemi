using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalCountRepository : IAnimalCountRepository
{
	private readonly DapperContext _context;

	public AnimalCountRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalCount>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalCount>(query, param);
		}
	}

	public async Task<AnimalCount> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalCount>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalCount animalCount)
	{
		var query = "INSERT INTO AnimalCount (GroupId, Count, CreationDate, IsActive) VALUES (@GroupId, @Count, @CreationDate, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalCount animalCount)
	{
		var query = "UPDATE AnimalCount SET GroupId = @GroupId, Count = @Count, CreationDate = @CreationDate, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, animalCount);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalCount WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
