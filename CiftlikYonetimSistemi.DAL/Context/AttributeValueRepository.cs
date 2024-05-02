using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AttributeValueRepository : IAttributeValueRepository
{
	private readonly DapperContext _context;

	public AttributeValueRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AttributeValue>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AttributeValue>(query, param);
		}
	}

	public async Task<AttributeValue> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AttributeValue>(query, param);
		}
	}
	public async Task<int> AddAsync(AttributeValue attributeValue)
	{
		var query = "INSERT INTO AttributeValue (AttributeTypeId, AttributeValue, CompanyUserMappingId, IsActive, CreationDate) VALUES (@AttributeTypeId, @AttributeValue, @CompanyUserMappingId, @IsActive, @CreationDate);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AttributeValue attributeValue)
	{
		var query = "UPDATE AttributeValue SET AttributeTypeId = @AttributeTypeId, AttributeValue = @AttributeValue, CompanyUserMappingId = @CompanyUserMappingId, IsActive = @IsActive, CreationDate = @CreationDate WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, attributeValue);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AttributeValue WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
