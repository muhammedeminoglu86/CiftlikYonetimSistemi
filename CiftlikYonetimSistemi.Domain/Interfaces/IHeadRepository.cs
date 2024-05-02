using CiftlikYonetimSistemi.Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IHeadRepository
	{
		Task<int> AddAsync(Head head, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(Head head, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<IEnumerable<Head>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<Head> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	}
}
