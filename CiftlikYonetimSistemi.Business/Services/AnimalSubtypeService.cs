using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using CiftlikYonetimSistemi.Extension;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.Services
{
    public class AnimalSubtypeService : IAnimalSubTypeService
    {
        private readonly IAnimalSubTypeRepository _animalSubtypeRepository;
        private readonly DapperContext _context;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly CreateMD5Hash _hashCreator;
        private readonly ICompanyUserMappingRepository _companyUserMappingRepository;

        public AnimalSubtypeService(IAnimalSubTypeRepository animalSubtypeRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, ICompanyUserMappingRepository companyUserMappingRepository)
        {
            _animalSubtypeRepository = animalSubtypeRepository;
            _context = context;
            _redisConnection = redisConnection;
            _companyUserMappingRepository = companyUserMappingRepository;
        }

        public async Task<int> AddAsync(AnimalSubType animalSubtype, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                var id = await _animalSubtypeRepository.AddAsync(animalSubtype, connection, transaction);

              

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(AnimalSubType animalSubtype)
        {
            try
            {
                await _animalSubtypeRepository.UpdateAsync(animalSubtype);

                string cacheKey = $"animalsubtype_{animalSubtype.Id}";
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _animalSubtypeRepository.DeleteAsync(id);

                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AnimalSubType> GetOne(string query, object param)
        {
            try
            {
                return await _animalSubtypeRepository.GetOne(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AnimalSubType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _animalSubtypeRepository.GetAllAsync(query, param, connection, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AnimalSubType AnimalSubtypeToDto(AnimalSubTypeDTO animalSubtype)
        {
            return new AnimalSubType
            {
                Id = animalSubtype.Id,
                Animalsubtypename = animalSubtype.Animalsubtypename,
                Animaltypeid = animalSubtype.Animaltypeid,
                Logo = animalSubtype.Logo
            };
        }
    }
}
