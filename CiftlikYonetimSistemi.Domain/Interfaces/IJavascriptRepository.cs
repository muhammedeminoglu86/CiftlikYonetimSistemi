using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Domain.Models;

public interface IJavascriptRepository
{
	Task<IEnumerable<Javascript>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	Task<Javascript> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	Task<int> AddAsync(Javascript javascript, IDbConnection connection = null, IDbTransaction transaction = null);
	Task UpdateAsync(Javascript javascript, IDbConnection connection = null, IDbTransaction transaction = null);
	Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
}
