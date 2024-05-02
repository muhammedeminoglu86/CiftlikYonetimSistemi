using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface ICompanyUserMappingRepository
	{
		Task<int> AddAsync(CompanyUserMapping user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(CompanyUserMapping user, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<IEnumerable<CompanyUserMapping>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<CompanyUserMapping> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
	}
}
