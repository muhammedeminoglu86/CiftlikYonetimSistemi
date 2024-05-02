using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Business.Services;
using CiftlikYonetimSistemi.Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CiftlikYonetimSistemi.Business.Interfaces;

namespace CiftlikYonetimSistemi.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICompanyUserMappingService _companyUserMappingService;
		private readonly IUserLoginService _userLoginService;
		public LoginController(IUserService _userService, ICompanyUserMappingService companyUserMappingService, IUserLoginService userLoginService)
		{
			this._userService = _userService;
			this._userLoginService = userLoginService;
		}
		public async Task<IActionResult> Login(UserDto user)
		{
			return View();
		}

		[HttpPost]
		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			if (loginDTO.Email != "" && loginDTO.Password != "")
			{
				var isValidUser = await _userService.ValidateLoginAsync(loginDTO);

				if (isValidUser != null)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, isValidUser.Username),
						new Claim(ClaimTypes.Email, isValidUser.Email),
						// Diğer claim'ler eklenebilir
					};

					var claimsIdentity = new ClaimsIdentity(
						claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var authProperties = new AuthenticationProperties
					{
						// Oturum süresi ve diğer özellikler burada tanımlanabilir
					};

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties);
					return RedirectToAction("Index", "Home");  // Redirect to a secure area
				}
				else
				{
					ModelState.AddModelError("", "Invalid username or password.");
					return View(loginDTO);
				}
			}

			// If we get here, something failed, redisplay form
			return View(loginDTO);
		}
	}
}
