using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalSubTypeRepository : IAnimalSubTypeRepository
{
	private readonly DapperContext _context;

	public AnimalSubTypeRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalSubType>(query, param);
		}
	}

	public async Task<AnimalSubType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalSubType>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalSubType subType)
	{
		var query = "INSERT INTO AnimalSubType (AnimalTypeId, AnimalSubTypeName, Logo, IsActive) VALUES (@AnimalTypeId, @AnimalSubTypeName, @Logo, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalSubType subType)
	{
		var query = "UPDATE AnimalSubType SET AnimalTypeId = @AnimalTypeId, AnimalSubTypeName = @AnimalSubTypeName, Logo = @Logo, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, subType);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalSubType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
