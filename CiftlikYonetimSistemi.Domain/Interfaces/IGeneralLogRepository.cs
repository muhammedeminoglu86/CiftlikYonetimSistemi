using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IGeneralLogRepository
	{
		Task<int> AddAsync(GeneralLog user);
		Task UpdateAsync(GeneralLog user);
		Task DeleteAsync(int id);
		Task<IEnumerable<GeneralLog>> GetAllAsync(string query, object param);
		Task<GeneralLog> GetOne(string query, object param);
	}
}
