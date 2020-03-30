using FoodTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodTracker.Data
{
	public class ImpExpDBContext : DbContext
	{
		private readonly IConfiguration iconf;
		public ImpExpDBContext(IConfiguration iconf)
		{			
			this.iconf = iconf;
		}

		public ImpExpDBContext()
		{

		}

		public DbSet<SubCategory> SubCategory { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Food> Foods { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{	
			string connectionString = iconf.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;			
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}