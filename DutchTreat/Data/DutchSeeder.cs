using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
	public class DutchSeeder
	{
		private readonly DutchContext Context;
		private readonly IHostingEnvironment hosting;

		public DutchSeeder(DutchContext context, IHostingEnvironment hosting)
		{
			this.Context = context;
			this.hosting = hosting;
		}

		public void Seed()
		{
			Context.Database.EnsureCreated();

			if (!Context.Products.Any())
			{
				string filepath = Path.Combine(hosting.ContentRootPath, "Data/art.json");
				string json = File.ReadAllText(filepath);
				var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				Context.AddRange(products);

				Order order = new Order()
				{
					OrderDate = DateTime.UtcNow,
					OrderNumber = "12345",
					Items = new List<OrderItem>()
					{
						new OrderItem()
						{
							Product = products.First(),
							Quantity = 5,
							UnitPrice = products.First().Price
						}
					}
				};
				Context.Add(order);
			}
			Context.SaveChanges();
		}
	}
}
