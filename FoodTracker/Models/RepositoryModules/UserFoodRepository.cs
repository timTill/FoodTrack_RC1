using FoodTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public class UserFoodRepository : FoodRepository, IUserFoodRepository
	{
		private readonly ApplicationDbContext context;
		public UserFoodRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Food>> GetAllFoodTypeByOwner(string userGUID)
		{
			var AllFoodTypesByOwner = await context.Foods.Include(m => m.Category)
				.Include(m => m.SubCategory)
				.Where(f => f.OwnerName == userGUID).ToListAsync();
			return AllFoodTypesByOwner;
		}

		public async Task<IEnumerable<Food>> GetAllRealFoodByOwner(string userGUID)
		{
			var AllRealFoodByOwner = await context.Foods.Where(m => m.QuantityLeft != 0 && m.OwnerName == userGUID).Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();

			return AllRealFoodByOwner;
		}

		public async Task<IEnumerable<Food>> GetAllFoodInCartByOwner(string userGUID)
		{
			var AllFoodInCartByOwner = await context.Foods
				.Where(m => m.IsInCart == true).Include(m => m.Category).
				Include(m => m.SubCategory).Where(f => f.OwnerName == userGUID).ToListAsync();
			return AllFoodInCartByOwner;
		}
	}
}
