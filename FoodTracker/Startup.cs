﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodTracker.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodTracker.Models;

namespace FoodTracker
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IDbInitializer, DbInitializer>();
			services.AddScoped<IDataManager, ImportExportManager>();

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddScoped<IFoodRepository, SQLFoodRepository>();			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				//app.UseStatusCodePages();
				//app.UseStatusCodePagesWithReExecute("/Admin/ErrorCheck/{0}");
			}
			else
			{
				//app.UseStatusCodePagesWithRedirects("Admin/Error/{0}");
				app.UseExceptionHandler("/Admin/ErrorCheck");
				app.UseStatusCodePagesWithReExecute("/Admin/ErrorCheck/{0}");
				// The default HSTS value is 30 days. You may want to change this for ú scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			dbInitializer.Initialize();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area=User}/{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

/*
namespace FridgeTracker
{
	public class Startup
	{
		
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddDbContextPool<AppDbContext>(options =>
			options.UseSqlServer(_config.GetConnectionString("FridgeFoodMSSQLConnectionTest")));

			
			services.AddMvc();
			services.AddScoped<IFridgeFoodRepository, SQLFridgeFoodRepository>();			
			//services.AddSingleton<IFridgeFoodRepository, MockFridgeFoodRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			dbInitializer.Initialize();
			app.UseDeveloperExceptionPage();
			app.UseStaticFiles();
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}


 */
