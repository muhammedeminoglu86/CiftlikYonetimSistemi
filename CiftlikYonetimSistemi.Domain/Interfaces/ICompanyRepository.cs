using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ICompanyRepository
	{
		Task<int> AddAsync(Company company, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(Company company, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<IEnumerable<Company>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<Company> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	}

}
