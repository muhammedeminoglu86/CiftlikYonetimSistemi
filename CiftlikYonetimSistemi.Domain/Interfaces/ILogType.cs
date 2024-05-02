using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ILogType
	{
		Task<int> AddAsync(LogType user);
		Task UpdateAsync(LogType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<LogType>> GetAllAsync(string query, object param);
		Task<LogType> GetOne(string query, object param);
	}
}
