using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Business.DTO;
using System.Data;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<int> AddAsync(AnimalTypeDTO animaltype);

        Task UpdateAsync(AnimalType user);
        Task DeleteAsync(int id);
        Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
        Task<AnimalType> GetOne(string query, object param);
        AnimalType AnimalTypeToDto(AnimalTypeDTO animalType);
    }
}
