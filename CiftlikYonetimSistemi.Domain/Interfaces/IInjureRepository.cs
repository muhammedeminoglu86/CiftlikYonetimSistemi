using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IInjureRepository
	{
		Task<int> AddAsync(Injure user);
		Task UpdateAsync(Injure user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Injure>> GetAllAsync(string query, object param);
		Task<Injure> GetOne(string query, object param);
	}
}
