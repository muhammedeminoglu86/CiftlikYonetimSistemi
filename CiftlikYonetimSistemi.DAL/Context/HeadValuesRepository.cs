using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.DAL.Context
{
	public class HeadValuesRepository : IHeadValuesRepository
	{
		private readonly DapperContext _context;

		public HeadValuesRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<HeadValues>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			try
			{
				return await conn.QueryAsync<HeadValues>(query, param, transaction);

			}
			catch (System.Exception ex)
			{
				return null;
			}
		}

		public async Task<HeadValues> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var conn = connection ?? _context.CreateConnection();
			return await conn.QueryFirstOrDefaultAsync<HeadValues>(query, param, transaction);
		}

		public async Task<int> AddAsync(HeadValues headValue, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"INSERT INTO HeadValue (HeadId, OrderNumber, Value, isActive) 
                          VALUES (@HeadId, @OrderNumber, @Value, @isActive);
                          SELECT CAST(SCOPE_IDENTITY() as int);";
			var conn = connection ?? _context.CreateConnection();
			var id = await conn.ExecuteScalarAsync<int>(query, headValue, transaction);
			return id;
		}

		public async Task UpdateAsync(HeadValues headValue, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = @"UPDATE HeadValue 
                          SET HeadId = @HeadId, OrderNumber= @OrderNumber, Value= @Value, isActive= @isActive
                          WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, headValue, transaction);
		}

		public async Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
		{
			var query = "DELETE FROM HeadValue WHERE Id = @Id";
			var conn = connection ?? _context.CreateConnection();
			await conn.ExecuteAsync(query, new { Id = id }, transaction);
		}
	}
}
