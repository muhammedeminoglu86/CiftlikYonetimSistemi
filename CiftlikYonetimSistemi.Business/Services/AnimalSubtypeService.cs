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
    public class AnimalSubTypeService : IAnimalSubTypeService
    {
        private readonly IAnimalSubTypeRepository _animalSubtypeRepository;
        private readonly DapperContext _context;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly CreateMD5Hash _hashCreator;
        private readonly ICompanyUserMappingRepository _companyUserMappingRepository;
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public AnimalSubTypeService(IAnimalSubTypeRepository animalSubtypeRepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash createMD5Hash, ICompanyUserMappingRepository companyUserMappingRepository, IAnimalTypeRepository animalTypeRepository)
        {
            _animalSubtypeRepository = animalSubtypeRepository;
            _context = context;
            _redisConnection = redisConnection;
            _companyUserMappingRepository = companyUserMappingRepository;
            _animalTypeRepository = animalTypeRepository;
        }
  
        public async Task<int> AddAsync(AnimalSubTypeDTO animalsubtype)
        {
            var animalsubtypex = AnimalSubtypeToDto(animalsubtype);
            IDbConnection connection = null;
            IDbTransaction transaction = null;

            var varmi = GetOne("select * from AnimalSubType where animaltypeid = @animaltypeid and animalsubtypename = @animalsubtypename", new { animaltypeid = animalsubtype.Animaltypeid, animalsubtypename = animalsubtype.Animalsubtypename }).Result;
            if (varmi != null)
                return -1;

            var varmix = _animalTypeRepository.GetOne("select * from AnimalType where id = @id", new { id = animalsubtypex.Animaltypeid }).Result;
            if (varmix == null)
                return -2;


			try
            {
                connection = _context.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                var id = await _animalSubtypeRepository.AddAsync(animalsubtypex, connection, transaction);
                transaction.Commit();

                return id;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;  // Rethrow the exception to handle it further up the call stack.
            }
            finally
            {
                transaction?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }


        public async Task UpdateAsync(AnimalSubTypeDTO animalSubtype)
        {
            try
            {

                var animalsubtypex = AnimalSubtypeToDto(animalSubtype);

                await _animalSubtypeRepository.UpdateAsync(animalsubtypex);


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
                Logo = animalSubtype.Logo,
                Isactive = 1
            };
        }
    }
}
