﻿using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.WebApi.Controller
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]

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
            foreach(var item in users)
            {
				item.Password = null;
			}
            return Ok(users);
        }

        [HttpGet("{id}/{email}")]
		public async Task<IActionResult> GetUserById(int id, string email)
		{
			User user = null;
			if (id != -1)
			{
				user = await _userService.GetOne("select * from User where id = @id", new { id = id });
			}
			else
			{
				user = await _userService.GetOne("select * from User where email = @email", new { email });
			}

			if (user == null)
				return NotFound();
			user.Password = null;
			return Ok(user);
		}

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            try
            {
                var createdUser = await _userService.AddAsync(user);
                if (createdUser == -1)
                    return BadRequest("Username already exists.");
                else if(createdUser == -2)
                    return BadRequest("Email already exists.");
                return Ok(new { Message = "User created successfully", UserId = createdUser });
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
