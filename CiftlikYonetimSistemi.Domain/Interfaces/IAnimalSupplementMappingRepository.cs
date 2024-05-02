using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalSupplementMappingRepository
	{
		Task<int> AddAsync(AnimalSupplementMapping user);
		Task UpdateAsync(AnimalSupplementMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalSupplementMapping>> GetAllAsync(string query, object param);
		Task<AnimalSupplementMapping> GetOne(string query, object param);
	}
}
