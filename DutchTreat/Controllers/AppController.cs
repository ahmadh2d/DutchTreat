using DutchTreat.Data;
using DutchTreat.ViewModels;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
	public class AppController : Controller
	{
		private readonly IMailService mailService;
		private readonly IDutchRepository repository;

		public AppController(IMailService mailService, IDutchRepository repository)
		{
			this.mailService = mailService;
			this.repository = repository;
		}

		public IActionResult Index()
		{
			//throw new InvalidOperationException();
			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			
			return View();
		}

		[HttpPost("contact")]
		public IActionResult Contact(ContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Send Email
				mailService.SendMessage("Ahmad2d@live.com", model.Subject, model.Message);
				ViewBag.UserMessage = "Mail Sent";
				ModelState.Clear();
			}
			else
			{
				// Show Error
			}

			return View();
		}

		[HttpGet("about")]
		public IActionResult About()
		{
			ViewBag.Title = "About Us";

			return View();
		}

		public IActionResult Shop()
		{
			var results = this.repository.GetAllProducts();

			return View(results.ToList());
		}
	}
}
