using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
	public class DutchRepository : IDutchRepository
	{
		private readonly DutchContext context;
		private readonly ILogger logger;

		public DutchRepository(DutchContext context, ILogger<DutchContext> logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public IEnumerable<Product> GetAllProducts()
		{
			try
			{
				this.logger.LogInformation("GetAllProducts Called!");

				return this.context.Products
					.OrderBy(p => p.Title)
					.ToList();
			}
			catch(Exception ex)
			{
				this.logger.LogError($"Failed to get all products: {ex}");
				return null;
			}

		}

		public IEnumerable<Product> GetProductsByCategory(string category)
		{

			return context.Products
				.Where(p => p.Category == category)
				.ToList();
		}

		public bool SaveAll()
		{
			return this.context.SaveChanges() > 0;
		}
	}
}
