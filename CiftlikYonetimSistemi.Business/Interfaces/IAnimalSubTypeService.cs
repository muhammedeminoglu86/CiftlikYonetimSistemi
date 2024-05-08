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
<<<<<<< HEAD
        Task<int> AddAsync(AnimalSubTypeDTO user);

        Task UpdateAsync(AnimalSubTypeDTO animalSubtype);
=======
        Task<int> AddAsync(AnimalSubType animaltype, IDbConnection connection, IDbTransaction transaction);
        Task UpdateAsync(AnimalSubType user);
>>>>>>> 06ae38778f6139afe083f76946bfe461a40b2f9c
        Task DeleteAsync(int id);
        Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null);
        Task<AnimalSubType> GetOne(string query, object param);
        AnimalSubType AnimalSubtypeToDto(AnimalSubTypeDTO animalSubtype);
    }
}
