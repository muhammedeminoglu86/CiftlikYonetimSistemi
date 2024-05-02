using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ISupplementRepository
	{
		Task<int> AddAsync(Supplement user);
		Task UpdateAsync(Supplement user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Supplement>> GetAllAsync(string query, object param);
		Task<Supplement> GetOne(string query, object param);
	}
}
