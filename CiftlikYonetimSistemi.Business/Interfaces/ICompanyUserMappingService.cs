using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
	public interface ICompanyUserMappingService
	{
		Task<int> AddAsync(CompanyUserMapping company);
		Task UpdateAsync(CompanyUserMapping company);
		Task DeleteAsync(int id);
		Task<IEnumerable<CompanyUserMapping>> GetAllAsync(string query, object param);
		Task<CompanyUserMapping> GetOne(string query, object param);
	}
}
