using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
    public interface IAnimalSubTypeService
    {
        Task<int> AddAsync(AnimalSubType animaltype, IDbConnection connection, IDbTransaction transaction);
        Task UpdateAsync(AnimalSubType user);
        Task DeleteAsync(int id);
        Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
        Task<AnimalSubType> GetOne(string query, object param);
        AnimalSubType AnimalSubtypeToDto(AnimalSubTypeDTO animalSubtype);
    }
}
