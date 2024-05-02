using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
	public interface ICompanyService
	{
		Task<int> AddAsync(Company company);
		Task UpdateAsync(Company company);
		Task DeleteAsync(int id);
		Task<IEnumerable<Company>> GetAllAsync(string query, object param);
		Task<Company> GetOne(string query, object param);
	}
}
