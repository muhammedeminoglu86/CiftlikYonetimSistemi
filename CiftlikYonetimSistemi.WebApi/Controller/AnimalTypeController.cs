using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Business.Interfaces;
using CiftlikYonetimSistemi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.WebApi.Controller
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]

    [ApiController]
    [Route("api/[controller]")]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeService _animalTypeService;

        public AnimalTypeController(IAnimalTypeService animaltypeService)
        {
            _animalTypeService = animaltypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var animalSubTypes = await _animalTypeService.GetAllAsync("select * from AnimalType where isactive = 1", null);
            return Ok(animalSubTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var animaltype = await _animalTypeService.GetOne("select * from AnimalType where id = @id and isactive = 1", new { id = id });
            if (animaltype == null)
                return NotFound();

            return Ok(animaltype);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AnimalTypeDTO animalsubtypedto)
        {
            try
            {
                animalsubtypedto.Logo = null;
                
                var createdUser = await _animalTypeService.AddAsync(animalsubtypedto);
                if (createdUser == -1)
                    return BadRequest("animal type already exists.");

                return Ok(new { Message = "Animal type created successfully", animalsubtypeid = createdUser });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateUser(AnimalType animaltype)
        {
            try
            {

                await _animalTypeService.UpdateAsync(animaltype);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
