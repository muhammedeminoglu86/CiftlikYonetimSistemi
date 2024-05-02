using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalRfidRepository
	{
		Task<int> AddAsync(AnimalRfid user);
		Task UpdateAsync(AnimalRfid user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AnimalRfid>> GetAllAsync(string query, object param);
		Task<AnimalRfid> GetOne(string query, object param);
	}
}
