using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAttributeTypeRepository
	{
		Task<int> AddAsync(AttributeType user);
		Task UpdateAsync(AttributeType user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AttributeType>> GetAllAsync(string query, object param);
		Task<AttributeType> GetOne(string query, object param);
	}
}
