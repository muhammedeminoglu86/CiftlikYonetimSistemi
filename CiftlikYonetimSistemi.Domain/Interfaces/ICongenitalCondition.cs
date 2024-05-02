using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ICongenitalCondition
	{
		Task<int> AddAsync(CongenitalCondition user);
		Task UpdateAsync(CongenitalCondition user);
		Task DeleteAsync(int id);
		Task<IEnumerable<CongenitalCondition>> GetAllAsync(string query, object param);
		Task<CongenitalCondition> GetOne(string query, object param);
	}
}
