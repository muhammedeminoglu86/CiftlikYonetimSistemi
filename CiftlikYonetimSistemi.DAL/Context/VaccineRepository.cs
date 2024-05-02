using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class VaccineRepository :IVaccineRepository
{
	private readonly DapperContext _context;

	public VaccineRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(Vaccine vaccine)
	{
		var query = @"
            INSERT INTO Vaccine (VaccineName, Text, Logo, IsActive) 
            VALUES (@VaccineName, @Text, @Logo, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, vaccine);
			return id;
		}
	}

	public async Task UpdateAsync(Vaccine vaccine)
	{
		var query = @"
            UPDATE Vaccine 
            SET VaccineName = @VaccineName, Text = @Text, Logo = @Logo, IsActive = @IsActive 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, vaccine);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM Vaccine WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<Vaccine>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<Vaccine>(query, param);
		}
	}

	public async Task<Vaccine> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<Vaccine>(query, param);
		}
	}
}
