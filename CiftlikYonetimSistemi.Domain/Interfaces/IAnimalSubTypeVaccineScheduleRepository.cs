using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalSubTypeVaccineScheduleRepository
	{
		Task<int> AddAsync(AnimalSubTypeVaccineSchedule user);
		Task UpdateAsync(AnimalSubTypeVaccineSchedule user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalSubTypeVaccineSchedule>> GetAllAsync(string query, object param);
		Task<AnimalSubTypeVaccineSchedule> GetOne(string query, object param);
	}
}
