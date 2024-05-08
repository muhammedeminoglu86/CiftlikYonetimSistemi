using CiftlikYonetimSistemi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Business.DTO;
using System.Data;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
>>>>>>> 06ae38778f6139afe083f76946bfe461a40b2f9c

namespace CiftlikYonetimSistemi.Business.Interfaces
{
    public interface IAnimalTypeService
    {
<<<<<<< HEAD
        Task<int> AddAsync(AnimalTypeDTO animaltype);
=======
        Task<int> AddAsync(AnimalType animaltype, IDbConnection connection, IDbTransaction transaction);
>>>>>>> 06ae38778f6139afe083f76946bfe461a40b2f9c
        Task UpdateAsync(AnimalType user);
        Task DeleteAsync(int id);
        Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
        Task<AnimalType> GetOne(string query, object param);
        AnimalType AnimalTypeToDto(AnimalTypeDTO animalType);
    }
}
