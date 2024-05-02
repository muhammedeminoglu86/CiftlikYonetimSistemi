using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalDiseaseMappingRepository
	{
		Task<int> AddAsync(AnimalDisaseMapping user);
		Task UpdateAsync(AnimalDisaseMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalDisaseMapping>> GetAllAsync(string query, object param);
		Task<AnimalDisaseMapping> GetOne(string query, object param);
	}
}
