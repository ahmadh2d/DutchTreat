using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat
{
	public class Startup
	{
		private readonly IConfiguration config;

		public Startup(IConfiguration config)
		{
			this.config = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<UserShop, IdentityRole>(cfg => {
				cfg.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<DutchContext>();

			services.AddTransient<IMailService, NullMailService>();
			// Support for real mail service

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddDbContext<DutchContext>(cfg => {
				cfg.UseSqlServer(config.GetConnectionString("DutchConnectionString"));
			});
			services.AddTransient<DutchSeeder>();
			services.AddScoped<IDutchRepository, DutchRepository>();

			services.AddControllersWithViews()
					.AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
					.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseStaticFiles();
			app.UseNodeModules();

			app.UseAuthentication();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(cfg =>
			{
				cfg.MapControllerRoute("Fallback",
					"{controller}/{action}/{id?}", new { controller = "App", action = "Index" });
				cfg.MapRazorPages();
			});
		}
	}
}
