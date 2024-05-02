using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IGroupRepository
	{
		Task<int> AddAsync(Group user);
		Task UpdateAsync(Group user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Group>> GetAllAsync(string query, object param);
		Task<Group> GetOne(string query, object param);
	}
}
