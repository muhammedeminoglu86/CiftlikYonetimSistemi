using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IDrugRepository
	{
		Task<int> AddAsync(Drug user);
		Task UpdateAsync(Drug user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Drug>> GetAllAsync(string query, object param);
		Task<Drug> GetOne(string query, object param);
	}
}
