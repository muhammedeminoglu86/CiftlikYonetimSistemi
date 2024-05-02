using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<int> AddAsync(User user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(User user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<IEnumerable<User>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<User> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	}

}
