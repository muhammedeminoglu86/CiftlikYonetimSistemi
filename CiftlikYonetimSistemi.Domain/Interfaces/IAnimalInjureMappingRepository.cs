using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalInjureMappingRepository
	{
		Task<int> AddAsync(AnimalInjureMapping user);
		Task UpdateAsync(AnimalInjureMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalInjureMapping>> GetAllAsync(string query, object param);
		Task<AnimalInjureMapping> GetOne(string query, object param);
	}
}
