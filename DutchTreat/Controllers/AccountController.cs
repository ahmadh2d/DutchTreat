using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> logger;
		private readonly SignInManager<UserShop> signInManager;

		public AccountController(ILogger<AccountController> logger, SignInManager<UserShop> signInManager)
		{
			this.logger = logger;
			this.signInManager = signInManager;
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
	}
}
