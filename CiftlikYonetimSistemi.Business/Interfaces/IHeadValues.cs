using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
	public interface IHeadValues
	{
		Task<int> AddAsync(HeadValues user);
		Task UpdateAsync(HeadValues user);
		Task DeleteAsync(int id);
		Task<IEnumerable<HeadValues>> GetAllAsync(string query, object param);
		Task<HeadValues> GetOne(string query, object param);
	}
}
