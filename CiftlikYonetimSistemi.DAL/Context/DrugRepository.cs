using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class DrugRepository : IDrugRepository
{
	private readonly DapperContext _context;

	public DrugRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Drug>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Drug>(query, param);
		}
	}

	public async Task<Drug> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Drug>(query, param);
		}
	}

	public async Task<int> AddAsync(Drug drug)
	{
		var query = @"INSERT INTO Drug (DrugName, DrugDescription, Logo, IsActive) VALUES (@DrugName, @DrugDescription, @Logo, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, drug);
			return id;
		}
	}

	public async Task UpdateAsync(Drug drug)
	{
		var query = "UPDATE Drug SET DrugName = @DrugName, DrugDescription = @DrugDescription, Logo = @Logo, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, drug);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Drug WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
