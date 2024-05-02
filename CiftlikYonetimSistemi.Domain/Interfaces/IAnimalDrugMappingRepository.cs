using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalDrugMappingRepository
	{
		Task<int> AddAsync(AnimalDrugMapping user);
		Task UpdateAsync(AnimalDrugMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalDrugMapping>> GetAllAsync(string query, object param);
		Task<AnimalDrugMapping> GetOne(string query, object param);
	}
}
