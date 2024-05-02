using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalTypeRepository
	{
		Task<int> AddAsync(AnimalType user);
		Task UpdateAsync(AnimalType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param);
		Task<AnimalType> GetOne(string query, object param);
	}
}
