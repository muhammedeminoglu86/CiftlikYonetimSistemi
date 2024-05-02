using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IHeadValuesRepository
	{
		Task<IEnumerable<HeadValues>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<HeadValues> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
		Task<int> AddAsync(HeadValues headValue, IDbConnection connection = null, IDbTransaction transaction = null);
		Task UpdateAsync(HeadValues headValue, IDbConnection connection = null, IDbTransaction transaction = null);
		Task DeleteAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
	}
}
