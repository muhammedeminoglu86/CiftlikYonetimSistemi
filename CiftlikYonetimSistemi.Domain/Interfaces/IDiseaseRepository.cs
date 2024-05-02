using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IDiseaseRepository
	{
		Task<int> AddAsync(Disase user);
		Task UpdateAsync(Disase user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Disase>> GetAllAsync(string query, object param);
		Task<Disase> GetOne(string query, object param);
	}
}
