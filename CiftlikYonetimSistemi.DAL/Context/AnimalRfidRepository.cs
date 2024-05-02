using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalRfidRepository : IAnimalRfidRepository
{
	private readonly DapperContext _context;

	public AnimalRfidRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalRfid>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalRfid>(query, param);
		}
	}

	public async Task<AnimalRfid> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalRfid>(query, param);
		}
	}
	public async Task<int> AddAsync(AnimalRfid rfid)
	{
		var query = "INSERT INTO AnimalRfid (AnimalId, RfidCode, IsActive, CreationDate) VALUES (@AnimalId, @RfidCode, @IsActive, @CreationDate);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalRfid rfid)
	{
		var query = "UPDATE AnimalRfid SET AnimalId = @AnimalId, RfidCode = @RfidCode, IsActive = @IsActive, CreationDate = @CreationDate WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, rfid);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalRfid WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
