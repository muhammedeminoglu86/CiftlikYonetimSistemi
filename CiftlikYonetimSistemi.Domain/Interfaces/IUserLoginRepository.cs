using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IUserLoginRepository
	{
		Task<int> AddAsync(UserLogin user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(UserLogin user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<IEnumerable<UserLogin>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<UserLogin> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	}
}
