using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalTypeRepository : IAnimalTypeRepository
{
	private readonly DapperContext _context;

	public AnimalTypeRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalType>(query, param);
		}
	}

	public async Task<AnimalType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalType>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalType type)
	{
		var query = "INSERT INTO AnimalType (AnimalType1, TypeDesc, Logo, IsActive, UserId) VALUES (@AnimalType1, @TypeDesc, @Logo, @IsActive, @UserId);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalType type)
	{
		var query = "UPDATE AnimalType SET AnimalType1 = @AnimalType1, TypeDesc = @TypeDesc, Logo = @Logo, IsActive = @IsActive, UserId = @UserId WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, type);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
