using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAttributeValueRepository
	{
		Task<int> AddAsync(AttributeValue user);
		Task UpdateAsync(AttributeValue user);
		Task DeleteAsync(int id);
		Task<IEnumerable<AttributeValue>> GetAllAsync(string query, object param);
		Task<AttributeValue> GetOne(string query, object param);
	}
}
