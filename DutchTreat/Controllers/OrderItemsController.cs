using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
	[Route("/api/orders/{orderid}/items")]
	public class OrderItemsController : Controller
	{
		private readonly IDutchRepository repository;
		private readonly ILogger<OrderItemsController> logger;
		private readonly IMapper mapper;

		public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
		{
			this.repository = repository;
			this.logger = logger;
			this.mapper = mapper;
		}

		[HttpGet]
		public IActionResult Get(int orderid)
		{
			var order = this.repository.GetOrderById(orderid);
			if (order != null)
			{
				return Ok(this.mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet("{id:int}")]
		public IActionResult Get(int orderid, int id)
		{
			var order = this.repository.GetOrderById(orderid);
			if (order != null)
			{
				var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
				if (item != null)
				{
					return Ok(this.mapper.Map<OrderItem, OrderItemViewModel>(item));
				}
			}

			return NotFound();
		}
	}
}
