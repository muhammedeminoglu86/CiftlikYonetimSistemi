using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class WeightRepository : IWeightRepository
{
	private readonly DapperContext _context;

	public WeightRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(Weight weight)
	{
		var query = @"
            INSERT INTO Weights (Weightx, AnimalId, CreationDate) 
            VALUES (@Weightx, @AnimalId, @CreationDate);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, new { weight.Weightx, weight.Animalid, weight.Creationdate });
			return id;
		}
	}

	public async Task UpdateAsync(Weight weight)
	{
		var query = @"
            UPDATE Weight 
            SET Weightx = @Weightx, AnimalId = @AnimalId, CreationDate = @CreationDate
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, weight);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Weight WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<Weight>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Weight>(query, param);
		}
	}

	public async Task<Weight> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Weight>(query, param);
		}
	}
}
