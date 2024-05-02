using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IGenderRepository
	{
		Task<int> AddAsync(Gender user);
		Task UpdateAsync(Gender user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Gender>> GetAllAsync(string query, object param);
		Task<Gender> GetOne(string query, object param);
	}
}
