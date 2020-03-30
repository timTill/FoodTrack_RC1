using FoodTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodTracker.Data
{
	public class ImpExpDBContext : DbContext
	{
		private readonly IConfiguration iconf;


		/*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)*/
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
			//var builder = new SqlConnectionStringBuilder(iservcoll.GetConnectionString())
			
			string connectionString = iconf.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
			//optionsBuilder.UseSqlServer("Data Source=localhost\\SZABSQL;Initial Catalog=FT_TestImpExp_Serv;Persist Security Info=True;User ID=szabi;Password=sqlpassword");			
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}

