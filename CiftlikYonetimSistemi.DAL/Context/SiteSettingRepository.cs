using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.DAL.Context;
using CiftlikYonetimSistemi.Domain.Interfaces;

public class SiteSettingRepository : ISiteSettingRepository
{
	private readonly DapperContext _context;

	public SiteSettingRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<int> AddAsync(SiteSetting siteSetting)
	{
		var query = @"
            INSERT INTO SiteSetting (SiteTitle, SiteDescription, SiteKeyword, SiteUrl, ControllerName, ActionName, SiteMode, IsActive) 
            VALUES (@SiteTitle, @SiteDescription, @SiteKeyword, @SiteUrl, @ControllerName, @ActionName, @SiteMode, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
		using (var connection = _context.CreateConnection())
		{
			var id = await connection.ExecuteScalarAsync<int>(query, siteSetting);
			return id;
		}
	}

	public async Task UpdateAsync(SiteSetting siteSetting)
	{
		var query = @"
            UPDATE SiteSetting 
            SET SiteTitle = @SiteTitle, SiteDescription = @SiteDescription, SiteKeyword = @SiteKeyword, 
                SiteUrl = @SiteUrl, ControllerName = @ControllerName, ActionName = @ActionName, 
                SiteMode = @SiteMode, IsActive = @IsActive 
            WHERE Id = @Id;
        ";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, siteSetting);
		}
	}

	public async Task DeleteAsync(int id)
	{
		var query = "DELETE FROM SiteSetting WHERE Id = @Id";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}

	public async Task<IEnumerable<SiteSetting>> GetAllAsync(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryAsync<SiteSetting>(query, param);
		}
	}

	public async Task<SiteSetting> GetOne(string query, object param)
	{
		using (var connection = _context.CreateConnection())
		{
			return await connection.QueryFirstOrDefaultAsync<SiteSetting>(query, param);
		}
	}
}
