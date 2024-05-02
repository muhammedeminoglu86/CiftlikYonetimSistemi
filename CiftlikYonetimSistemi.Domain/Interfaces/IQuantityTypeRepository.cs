using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IQuantityTypeRepository
	{
		Task<int> AddAsync(QuantityType user);
		Task UpdateAsync(QuantityType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<QuantityType>> GetAllAsync(string query, object param);
		Task<QuantityType> GetOne(string query, object param);
	}
}
