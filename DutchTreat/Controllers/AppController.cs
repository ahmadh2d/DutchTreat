using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;
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

		public AppController(IMailService mailService)
		{
			this.mailService = mailService;
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
		public IActionResult Contact(ContactModel model)
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
	}
}
