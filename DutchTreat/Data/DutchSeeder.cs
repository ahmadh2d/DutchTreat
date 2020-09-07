using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<UserShop> userManager;

		public DutchSeeder(DutchContext context, IHostingEnvironment hosting, UserManager<UserShop> userManager)
		{
			this.Context = context;
			this.hosting = hosting;
			this.userManager = userManager;
		}

		public async Task SeedAsync()
		{
			Context.Database.EnsureCreated();

			UserShop user = await this.userManager.FindByEmailAsync("ahmad2d@live.com");

			if (user == null)
			{
				user = new UserShop
				{
					FirstName = "Ahmad",
					LastName = "Haseeb",
					Email = "ahmad2d@live.com",
					UserName = "ahmad2d_admin"
				};

				var result = await this.userManager.CreateAsync(user, "P@ssw0rd!");
				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Could not create new user In Seeder");
				}
			}

			if (!Context.Products.Any())
			{
				string filepath = Path.Combine(hosting.ContentRootPath, "Data/art.json");
				string json = File.ReadAllText(filepath);
				var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				Context.AddRange(products);

				Order order = new Order()
				{
					User = user,
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
