using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync("select * from User", null);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetOne("select * from User where id = @id", new {id = id});
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                var createdUser = await _userService.AddAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var user = await _userService.ValidateLoginAsync(loginDTO);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Burada token oluşturma ve dönme işlemleri yapılabilir. Örnek olarak basit bir OK dönüş yapıyoruz.
            return Ok(new { Message = "Login successful", UserId = user.Id });
        }

    }
}
