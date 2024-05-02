using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
	public interface IUserLoginService
	{
		Task<int> AddAsync(UserLogin user);
		Task UpdateAsync(UserLogin user);
		Task DeleteAsync(int id);
		Task<IEnumerable<UserLogin>> GetAllAsync(string query, object param);
		Task<UserLogin> GetOne(string query, object param);
	}
}
