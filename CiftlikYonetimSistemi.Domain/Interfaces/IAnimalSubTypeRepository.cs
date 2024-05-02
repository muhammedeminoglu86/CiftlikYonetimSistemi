using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalSubTypeRepository
	{

		Task<int> AddAsync(AnimalSubType user);
		Task UpdateAsync(AnimalSubType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param);
		Task<AnimalSubType> GetOne(string query, object param);

	}
}
