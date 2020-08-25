using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class OrdersController : Controller
	{
		private readonly IDutchRepository repository;
		private readonly ILogger<OrdersController> logger;

		public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
		{
			this.repository = repository;
			this.logger = logger;
		}

		[HttpGet("{id:int}")]
		public ActionResult<Order> Get(int id)
		{
			try
			{
				var Order = this.repository.GetOrder(id);

				if (Order != null) return Ok(Order);
				else return NotFound();
			}
			catch(Exception ex)
			{
				this.logger.LogError($"Failed to get order: {ex}");
				return BadRequest("Failed to get order");
			}
		}
	}
}
