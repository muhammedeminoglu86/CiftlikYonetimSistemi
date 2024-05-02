using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalRepository
	{
		Task<int> AddAsync(Animal user);
		Task UpdateAsync(Animal user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Animal>> GetAllAsync(string query, object param);
		Task<Animal> GetByIdAsync(string query, object param);
	}
}
