using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalCongenitalConditionMappingRepository
	{
		Task<int> AddAsync(AnimalCongenitalConditionMapping user);
		Task UpdateAsync(AnimalCongenitalConditionMapping user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalCongenitalConditionMapping>> GetAllAsync(string query, object param);
		Task<AnimalCongenitalConditionMapping> GetOne(string query, object param);
	}
}
