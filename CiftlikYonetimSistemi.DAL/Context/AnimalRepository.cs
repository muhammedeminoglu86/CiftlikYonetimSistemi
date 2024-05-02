using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalRepository : IAnimalRepository
{
	private readonly DapperContext _context;

	public AnimalRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Animal>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Animal>(query, param);
		}
	}

	public async Task<Animal> GetByIdAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Animal>(query, param);
		}
	}

	public async Task<int> AddAsync(Animal animal)
	{
		var query = "INSERT INTO Animal (AnimalName, AnimalDescription, UniqueGuid, CompanyUserMappingId, IsActive, CreationDate, ParentAnimalId, AnimalSubTypeId, BirthDate) VALUES (@AnimalName, @AnimalDescription, @UniqueGuid, @CompanyUserMappingId, @IsActive, @CreationDate, @ParentAnimalId, @AnimalSubTypeId, @BirthDate);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(Animal animal)
	{
		var query = "UPDATE Animal SET AnimalName = @AnimalName, AnimalDescription = @AnimalDescription, UniqueGuid = @UniqueGuid, CompanyUserMappingId = @CompanyUserMappingId, IsActive = @IsActive, CreationDate = @CreationDate, ParentAnimalId = @ParentAnimalId, AnimalSubTypeId = @AnimalSubTypeId, BirthDate = @BirthDate WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, animal);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Animal WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
