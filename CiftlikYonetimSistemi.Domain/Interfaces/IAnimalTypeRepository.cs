using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Interfaces
{
	public interface IAnimalTypeRepository
	{
		Task<int> AddAsync(AnimalType type, IDbConnection connection, IDbTransaction transaction);
		Task UpdateAsync(AnimalType user);
		Task DeleteAsync(int id);
		Task<AnimalType> GetOne(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);    
		Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);

    } 
}
