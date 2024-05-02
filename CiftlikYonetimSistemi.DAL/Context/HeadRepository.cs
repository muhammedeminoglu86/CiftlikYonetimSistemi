using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.DAL.Context
{
	public class HeadRepository : IHeadRepository
	{
		private readonly DapperContext _context;

		public HeadRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Head>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			return await conn.QueryAsync<Head>(query, param, transaction);
		}

		public async Task<Head> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			return await conn.QueryFirstOrDefaultAsync<Head>(query, param, transaction);
		}

		public async Task<int> AddAsync(Head head, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"INSERT INTO Head (ControllerName, ActionName, Title, Charset, Description, OgLocale, OgType, OgTitle, OgUrl, OgSiteName, Canonical, IsActive, CreationDate) 
                          VALUES (@ControllerName, @ActionName, @Title, @Charset, @Description, @OgLocale, @OgType, @OgTitle, @OgUrl, @OgSiteName, @Canonical, @IsActive, @CreationDate);
                          SELECT CAST(SCOPE_IDENTITY() as int);";
			var conn = connection ?? _context.CreateConnection();
			var id = await conn.ExecuteScalarAsync<int>(query, head, transaction);
			return id;
		}

		public async Task UpdateAsync(Head head, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"UPDATE Head 
                          SET ControllerName = @ControllerName, ActionName = @ActionName, Title = @Title, Charset = @Charset, Description = @Description, 
                              OgLocale = @OgLocale, OgType = @OgType, OgTitle = @OgTitle, OgUrl = @OgUrl, OgSiteName = @OgSiteName, 
                              Canonical = @Canonical, IsActive = @IsActive, CreationDate = @CreationDate 
                          WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, head, transaction);
		}

		public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = "DELETE FROM Head WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, new { Id = id }, transaction);
		}
	}
}
