using DutchTreat.Models;
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
