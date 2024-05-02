using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class UserTypeRepository : IUserTypeRepository
{
	private readonly DapperContext _context;

	public UserTypeRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(UserType user)
	{
		var query = @"
            INSERT INTO UserType (usertypename, usertypedescription, IsActive) 
            VALUES (@usertypename, @usertypedescription, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, user);
			return id;
		}
	}

	public async Task UpdateAsync(UserType user)
	{
		var query = @"
            UPDATE UserType
            SET usertypename = @usertypename, usertypedescription = @usertypedescription, IsActive = @IsActive
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, user);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM UserType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<UserType>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<UserType>(query, param);
		}
	}

	public async Task<UserType> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<UserType>(query, param);
		}
	}
}
