using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalCountRepository
	{
		Task<int> AddAsync(AnimalCount user);
		Task UpdateAsync(AnimalCount user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalCount>> GetAllAsync(string query, object param);
		Task<AnimalCount> GetOne(string query, object param);
	}
}
