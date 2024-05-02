using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class AnimalSubTypeVaccineScheduleRepository : IAnimalSubTypeVaccineScheduleRepository
{
	private readonly DapperContext _context;

	public AnimalSubTypeVaccineScheduleRepository(DapperContext context)
	{
		_context = context;
	}
	public async Task<IEnumerable<AnimalSubTypeVaccineSchedule>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<AnimalSubTypeVaccineSchedule>(query, param);
		}
	}

	public async Task<AnimalSubTypeVaccineSchedule> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<AnimalSubTypeVaccineSchedule>(query, param);
		}
	}

	public async Task<int> AddAsync(AnimalSubTypeVaccineSchedule schedule)
	{
		var query = "INSERT INTO AnimalSubTypeVaccineSchedule (AnimalSubTypeId, VaccineId, CreationDate, PlannedDate, IsDone, CompanyUserMapId, IsActive) VALUES (@AnimalSubTypeId, @VaccineId, @CreationDate, @PlannedDate, @IsDone, @CompanyUserMapId, @IsActive);SELECT CAST(SCOPE_IDENTITY() as int);";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query);
			return id;
		}
	}

	public async Task UpdateAsync(AnimalSubTypeVaccineSchedule schedule)
	{
		var query = "UPDATE AnimalSubTypeVaccineSchedule SET AnimalSubTypeId = @AnimalSubTypeId, VaccineId = @VaccineId, CreationDate = @CreationDate, PlannedDate = @PlannedDate, IsDone = @IsDone, CompanyUserMapId = @CompanyUserMapId, IsActive = @IsActive WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, schedule);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM AnimalSubTypeVaccineSchedule WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
