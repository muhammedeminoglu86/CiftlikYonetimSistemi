using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IUserTypeRepository
	{
		Task<int> AddAsync(UserType user);
		Task UpdateAsync(UserType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<UserType>> GetAllAsync(string query, object param);
		Task<UserType> GetOne(string query, object param);
	}
}
