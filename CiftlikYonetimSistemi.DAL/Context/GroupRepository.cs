using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class GroupRepository : IGroupRepository
{
	private readonly DapperContext _context;

	public GroupRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Group>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Group>(query, param);
		}
	}

	public async Task<Group> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Group>(query, param);
		}
	}
	public async Task<int> AddAsync(Group group)
	{
		var query = @"
            INSERT INTO Group (GroupName, GroupDescription, IsActive, CompanyUserMappingId) 
            VALUES (@GroupName, @GroupDescription, @IsActive, @CompanyUserMappingId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, group);
			return id;
		}
	}

	public async Task UpdateAsync(Group group)
	{
		var query = @"
            UPDATE Group 
            SET GroupName = @GroupName, GroupDescription = @GroupDescription, IsActive = @IsActive, CompanyUserMappingId = @CompanyUserMappingId 
            WHERE Id = @Id
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, group);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Group WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
