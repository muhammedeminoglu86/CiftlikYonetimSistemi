using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IWeaningRepository
	{
		Task<int> AddAsync(Weaning user);
		Task UpdateAsync(Weaning user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Weaning>> GetAllAsync(string query, object param);
		Task<Weaning> GetOne(string query, object param);
	}
}
