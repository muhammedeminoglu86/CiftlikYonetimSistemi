using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
	public interface IHeadService
	{
		Task<int> AddAsync(Head user);
		Task UpdateAsync(Head user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Head>> GetAllAsync(string query, object param);
		Task<Head> GetOne(string query, object param);
	}
}
