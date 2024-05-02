using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AttributeTypeRepository : IAttributeTypeRepository
{
	private readonly DapperContext _context;

	public AttributeTypeRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AttributeType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AttributeType>(query, param);
		}
	}

	public async Task<AttributeType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AttributeType>(query, param);
		}
	}

	public async Task<int> AddAsync(AttributeType attributeType)
	{
		var query = "INSERT INTO AttributeType (AttributeTypeName, AttributeTypeDescription, IsUnique, IsActive, QuantityTypeId) VALUES (@AttributeTypeName, @AttributeTypeDescription, @IsUnique, @IsActive, @QuantityTypeId);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AttributeType attributeType)
	{
		var query = "UPDATE AttributeType SET AttributeTypeName = @AttributeTypeName, AttributeTypeDescription = @AttributeTypeDescription, IsUnique = @IsUnique, IsActive = @IsActive, QuantityTypeId = @QuantityTypeId WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, attributeType);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AttributeType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
