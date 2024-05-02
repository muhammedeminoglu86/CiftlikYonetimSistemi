using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IWeightRepository
	{
		Task<int> AddAsync(Weight user);
		Task UpdateAsync(Weight user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Weight>> GetAllAsync(string query, object param);
		Task<Weight> GetOne(string query, object param);
	}
}
