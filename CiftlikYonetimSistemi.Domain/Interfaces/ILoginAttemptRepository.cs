using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ILoginAttemptRepository
	{
		Task<int> AddAsync(LoginAttempt user);
		Task UpdateAsync(LoginAttempt user);
		Task DeleteAsync(int id);
		Task<IEnumerable<LoginAttempt>> GetAllAsync(string query, object param);
		Task<LoginAttempt> GetOne(string query, object param);
	}
}
