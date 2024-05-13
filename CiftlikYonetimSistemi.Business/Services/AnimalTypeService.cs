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
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly IAnimalTypeRepository _animaltyperepository;
        private readonly DapperContext _context;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly CreateMD5Hash _hashCreator;
        private readonly ICompanyUserMappingRepository _companyUserMappingRepository;

        public AnimalTypeService(IAnimalTypeRepository animalrepository, DapperContext context, IConnectionMultiplexer redisConnection, CreateMD5Hash hashCreator, ICompanyUserMappingRepository companyUserMappingRepository)
        {
            _animaltyperepository = animalrepository;
            _context = context;
            _redisConnection = redisConnection;
            _hashCreator = hashCreator;
            _companyUserMappingRepository = companyUserMappingRepository;
        }


        public async Task<int> AddAsync(AnimalTypeDTO animaltype)
        {
            var animaltypex = AnimalTypeToDto(animaltype);
            IDbConnection connection = null;
            IDbTransaction transaction = null;

            var varmi = GetOne("select * from AnimalType where animaltype = @animaltype", new { animaltype = animaltype.animaltype }).Result;
            
            
            if (varmi != null)
                return -1;

            try
            {
                connection = _context.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                var id = await _animaltyperepository.AddAsync(animaltypex, connection, transaction);
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


        public async Task<int> AddAsync(AnimalType animaltype, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                var id = await _animaltyperepository.AddAsync(animaltype, connection, transaction);
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(AnimalType animaltype)
        {
            try
            {
                await _animaltyperepository.UpdateAsync(animaltype);
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
                await _animaltyperepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AnimalType> GetOne(string query, object param)
        {
            try
            {
                return await _animaltyperepository.GetOne(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AnimalType>> GetAllAsync(string query, object param, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _animaltyperepository.GetAllAsync(query, param, connection, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AnimalType AnimalTypeToDto(AnimalTypeDTO animalType)
        {
            return new AnimalType
            {
                Id = animalType.id,

                Animaltype = animalType.animaltype,
                Typedesc = animalType.typedesc,
                Userid = -1,
                isactive = 1,
            };
        }


    }
}
