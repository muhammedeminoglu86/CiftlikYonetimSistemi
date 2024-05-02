using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalVaccineMappingRepository
	{
		Task<int> AddAsync(AnimalVaccineMapping user);
		Task UpdateAsync(AnimalVaccineMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalVaccineMapping>> GetAllAsync(string query, object param);
		Task<AnimalVaccineMapping> GetOne(string query, object param);
	}
}
