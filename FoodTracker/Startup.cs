using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTracker
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
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
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
			services.AddScoped<IFoodRepository, FoodRepository>();
			services.AddScoped<IUserFoodRepository, UserFoodRepository>();		
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{				
				app.UseExceptionHandler("/Admin/ErrorCheck");
				app.UseStatusCodePagesWithReExecute("/Admin/ErrorCheck/{0}");				
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