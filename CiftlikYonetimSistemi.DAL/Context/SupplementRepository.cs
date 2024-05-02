using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class SupplementRepository : ISupplementRepository
{
	private readonly DapperContext _context;

	public SupplementRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(Supplement supplement)
	{
		var query = @"
            INSERT INTO Supplement (SupplementName, SupplementDescription, Blob, IsActive) 
            VALUES (@SupplementName, @SupplementDescription, @Blob, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, supplement);
			return id;
		}
	}

	public async Task UpdateAsync(Supplement supplement)
	{
		var query = @"
            UPDATE Supplement 
            SET SupplementName = @SupplementName, SupplementDescription = @SupplementDescription, Blob = @Blob, IsActive = @IsActive 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, supplement);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Supplement WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<Supplement>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Supplement>(query, param);
		}
	}

	public async Task<Supplement> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Supplement>(query, param);
		}
	}
}
