﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DutchTreat.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> logger;
		private readonly SignInManager<UserShop> signInManager;
		private readonly UserManager<UserShop> userManager;
		private readonly IConfiguration config;

		public AccountController(ILogger<AccountController> logger, SignInManager<UserShop> signInManager,
			UserManager<UserShop> userManager, IConfiguration config)
		{
			this.logger = logger;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.config = config;
		}

		public IActionResult Login()
		{
			if (this.User.Identity.IsAuthenticated)
				RedirectToAction("Index", "App");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					if (Request.Query.Keys.Contains("ReturnUrl"))
					{
						return Redirect(Request.Query["ReturnUrl"].First());
					}
					else
					{
						return RedirectToAction("Shop", "App");
					}
				}
			}

			ModelState.AddModelError("", "Failed to Login.");

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();
			return RedirectToAction("Index", "App");
		}

		[HttpPost]
		public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await this.userManager.FindByNameAsync(model.Username);

				if (user != null)
				{
					var result = await this.signInManager.CheckPasswordSignInAsync(user, model.Password, false);

					if (result.Succeeded)
					{
						var claims = new[]
						{
							new Claim(JwtRegisteredClaimNames.Sub, user.Email),
							new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
							new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
						};

						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
						var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

						var token = new JwtSecurityToken
						(
							config["Tokens:Issuer"],
							config["Tokens:Audience"],
							claims,
							expires: DateTime.UtcNow.AddMinutes(30),
							signingCredentials: creds
						);

						var results = new
						{
							token= new JwtSecurityTokenHandler().WriteToken(token),
							expiration = token.ValidTo
						};

						return Created("", results);
					}
				}
			}

			return BadRequest();
		}
	}
}
