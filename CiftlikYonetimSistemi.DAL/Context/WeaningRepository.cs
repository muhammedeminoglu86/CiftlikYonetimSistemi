using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class WeaningRepository :IWeaningRepository
{
	private readonly DapperContext _context;

	public WeaningRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(Weaning weaning)
	{
		var query = @"
            INSERT INTO Weaning (AnimalId, WeaningDate, IsActive, CompanyUserMappingId) 
            VALUES (@AnimalId, @WeaningDate, @IsActive, @CompanyUserMappingId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, weaning);
			return id;
		}
	}

	public async Task UpdateAsync(Weaning weaning)
	{
		var query = @"
            UPDATE Weaning 
            SET AnimalId = @AnimalId, WeaningDate = @WeaningDate, IsActive = @IsActive, 
                CompanyUserMappingId = @CompanyUserMappingId 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, weaning);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Weaning WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<Weaning>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Weaning>(query, param);
		}
	}

	public async Task<Weaning> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Weaning>(query, param);
		}
	}
}
