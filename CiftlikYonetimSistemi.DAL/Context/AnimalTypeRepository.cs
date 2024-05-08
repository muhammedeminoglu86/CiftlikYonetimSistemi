using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalTypeRepository : IAnimalTypeRepository
{
	private readonly DapperContext _context;

	public AnimalTypeRepository(DapperContext context)
	{
		_context = context;
	}


    public async Task<AnimalType> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
    {
        var conn = connection ?? _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AnimalType>(query, param, transaction);
    }

    public async Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
    {
        var conn = connection ?? _context.CreateConnection();
        return await conn.QueryAsync<AnimalType>(query, param, transaction);
    }


    public async Task<int> AddAsync(AnimalType type, IDbConnection connection, IDbTransaction transaction)
    {

        var query = @"INSERT INTO AnimalType (animaltype, typedesc, logo, isactive, userid) 
        VALUES (@animaltype, @typedesc, @logo, @isactive, @userid);
        SELECT LAST_INSERT_ID();";
        // No new connection is created here, we use the provided one.
        try
        {
            var id = await connection.ExecuteScalarAsync<int>(query, type, transaction);
            return id;
        }
        catch(Exception ex)
        {
            return 0;
        }



    }

    public async Task UpdateAsync(AnimalType type)
	{
		var query = "UPDATE AnimalType SET AnimalType1 = @AnimalType1, TypeDesc = @TypeDesc, Logo = @Logo, IsActive = @IsActive, UserId = @UserId WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, type);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalType WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
