using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[Controller]")]
	//[ApiController]
	public class OrdersController : Controller
	{
		private readonly IDutchRepository repository;
		private readonly ILogger<OrdersController> logger;
		private readonly IMapper mapper;
		private readonly UserManager<UserShop> userManager;

		public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger,
			IMapper mapper, UserManager<UserShop> userManager)
		{
			this.repository = repository;
			this.logger = logger;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Order>> Get(bool includeItems = true)
		{
			try
			{
				var username = User.Identity.Name;
				var result = this.repository.GetAllOrdersByUser(username, includeItems);

				return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(result));
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Failed to get orders: {ex}");
				return BadRequest("Failed to get orders");
			}
		}

		[HttpGet("{id:int}")]
		public ActionResult<Order> Get(int id)
		{
			try
			{
				var username = User.Identity.Name;
				var order = this.repository.GetOrderById(username, id);

				if (order != null) return Ok(mapper.Map<Order, OrderViewModel>(order));
				else return NotFound();
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Failed to get order: {ex}");
				return BadRequest("Failed to get order");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] OrderViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Order newOrder = this.mapper.Map<OrderViewModel, Order>(model);
					if (newOrder.OrderDate == DateTime.MinValue)
					{
						newOrder.OrderDate = DateTime.Now;
					}

					var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
					newOrder.User = currentUser;

					this.repository.AddOrder(newOrder);

					if (this.repository.SaveAll())
					{
						return Created($"/api/orders/{newOrder.Id}", this.mapper.Map<Order, OrderViewModel>(newOrder));
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Failed to create new Order: {ex}");
			}

			return BadRequest("Failed to create new Order");
		}
	}
}
