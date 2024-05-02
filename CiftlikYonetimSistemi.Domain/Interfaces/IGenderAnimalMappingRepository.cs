using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IGenderAnimalMappingRepository
	{
		Task<int> AddAsync(GenderAnimalMapping user);
		Task UpdateAsync(GenderAnimalMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<GenderAnimalMapping>> GetAllAsync(string query, object param);
		Task<GenderAnimalMapping> GetOne(string query, object param);
	}
}
