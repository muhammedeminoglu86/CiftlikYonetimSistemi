using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IVaccineRepository
	{
		Task<int> AddAsync(Vaccine user);
		Task UpdateAsync(Vaccine user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Vaccine>> GetAllAsync(string query, object param);
		Task<Vaccine> GetOne(string query, object param);
	}
}
