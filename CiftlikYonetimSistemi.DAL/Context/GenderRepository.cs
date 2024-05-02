using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class GenderRepository : IGenderRepository
{
	private readonly DapperContext _context;

	public GenderRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Gender>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Gender>(query, param);
		}
	}

	public async Task<Gender> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Gender>(query, param);
		}
	}

	public async Task<int> AddAsync(Gender gender)
	{
		var query = @"INSERT INTO Gender (GenderName, IsActive) VALUES (@GenderName, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, gender);
			return id;
		}
	}

	public async Task UpdateAsync(Gender gender)
	{
		var query = "UPDATE Gender SET GenderName = @GenderName, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, gender);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Gender WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
