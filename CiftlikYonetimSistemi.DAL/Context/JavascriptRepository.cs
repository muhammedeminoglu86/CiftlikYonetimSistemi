using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.DAL.Context
{
	public class JavascriptRepository : IJavascriptRepository
	{
		private readonly DapperContext _context;

		public JavascriptRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Javascript>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			return await conn.QueryAsync<Javascript>(query, param, transaction);
		}

		public async Task<Javascript> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			return await conn.QueryFirstOrDefaultAsync<Javascript>(query, param, transaction);
		}

		public async Task<int> AddAsync(Javascript Javascript, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"INSERT INTO Javascript (ControllerName, ActionName, Value, IsActive, OrderNumber) 
                          VALUES (@ControllerName, @ActionName, @Value, @IsActive, @OrderNumber);
                          SELECT CAST(SCOPE_IDENTITY() as int);";
			var conn = connection ?? _context.CreateConnection();
			var id = await conn.ExecuteScalarAsync<int>(query, Javascript, transaction);
			return id;
		}

		public async Task UpdateAsync(Javascript Javascript, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"UPDATE Javascript 
                          SET ControllerName = @ControllerName, ActionName = @ActionName, Value = @Value, IsActive = @IsActive, OrderNumber = @OrderNumber 
                          WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, Javascript, transaction);
		}

		public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = "DELETE FROM Javascript WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, new { Id = id }, transaction);
		}
	}
}
