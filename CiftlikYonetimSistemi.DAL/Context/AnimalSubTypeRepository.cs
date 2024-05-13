using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalSubTypeRepository : IAnimalSubTypeRepository
{
	private readonly DapperContext _context;

	public AnimalSubTypeRepository(DapperContext context)
	{
		_context = context;
	}

    public async Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
    {
        var conn = connection ?? _context.CreateConnection();
        return await conn.QueryAsync<AnimalSubType>(query, param, transaction);
    }

    public async Task<AnimalSubType> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
    {
        var conn = connection ?? _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AnimalSubType>(query, param, transaction);
    }

   
    public async Task<int> AddAsync(AnimalSubType user, IDbConnection connection, IDbTransaction transaction)
    {
        var query = @"INSERT INTO AnimalSubType (Animaltypeid, Animalsubtypename, Logo, isactive) VALUES (@Animaltypeid, @Animalsubtypename, @Logo, 1);
        SELECT LAST_INSERT_ID();";
        // No new connection is created here, we use the provided one.
        var id = await connection.ExecuteScalarAsync<int>(query, user, transaction);
        return id;
    }

    public async Task UpdateAsync(AnimalSubType subType)
	{
		var query = "UPDATE AnimalSubType SET AnimalTypeId = @AnimalTypeId, AnimalSubTypeName = @AnimalSubTypeName, Logo = @Logo, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, subType);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalSubType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
