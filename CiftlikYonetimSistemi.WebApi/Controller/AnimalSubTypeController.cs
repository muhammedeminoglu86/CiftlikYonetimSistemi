using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.Domain.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.WebApi.Controller
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]

    [ApiController]
    [Route("api/[controller]")]
    public class AnimalSubTypeController : ControllerBase
    {
        private readonly IAnimalSubTypeService _animalSubTypeService;

        public AnimalSubTypeController(IAnimalSubTypeService animalSubTypeService)
        {
            _animalSubTypeService = animalSubTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var animalSubTypes = await _animalSubTypeService.GetAllAsync("select * from AnimalSubType", null);
            return Ok(animalSubTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var animalSubTye = await _animalSubTypeService.GetOne("select * from User where id = @id", new { id = id });
            if (animalSubTye == null)
                return NotFound();

            return Ok(animalSubTye);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AnimalSubTypeDTO animalsubtype)
        {
            try
            {
                var createdUser = await _animalSubTypeService.AddAsync(animalsubtype);
                if (createdUser == -1)
                    return BadRequest("animalsubtype already exists.");
               
                return Ok(new { Message = "Animal Subtype created successfully", UserId = createdUser });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateUser(AnimalSubTypeDTO animaltypedto)
        {
            try
            {
                await _animalSubTypeService.UpdateAsync(animaltypedto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
    }
}
