using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class CongenitalConditionRepository : ICongenitalCondition
{
	private readonly DapperContext _context;

	public CongenitalConditionRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<CongenitalCondition>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<CongenitalCondition>(query, param);
		}
	}

	public async Task<CongenitalCondition> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<CongenitalCondition>(query, param);
		}
	}
	public async Task<int> AddAsync(CongenitalCondition condition)
	{
		var query = "INSERT INTO CongenitalCondition (CongenitalConditionName, CongenitalConditionDescription, IsActive) VALUES (@CongenitalConditionName, @CongenitalConditionDescription, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(CongenitalCondition condition)
	{
		var query = "UPDATE CongenitalCondition SET CongenitalConditionName = @CongenitalConditionName, CongenitalConditionDescription = @CongenitalConditionDescription, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, condition);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM CongenitalCondition WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
