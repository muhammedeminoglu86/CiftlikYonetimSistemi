using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalGroupMappingRepository
	{
		Task<int> AddAsync(AnimalGroupMapping user);
		Task UpdateAsync(AnimalGroupMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalGroupMapping>> GetAllAsync(string query, object param);
		Task<AnimalGroupMapping> GetOne(string query, object param);
	}
}
