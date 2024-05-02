using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ISiteSettingRepository
	{
		Task<int> AddAsync(SiteSetting user);
		Task UpdateAsync(SiteSetting user);
		Task DeleteAsync(int id);
		Task<IEnumerable<SiteSetting>> GetAllAsync(string query, object param);
		Task<SiteSetting> GetOne(string query, object param);
	}
}
