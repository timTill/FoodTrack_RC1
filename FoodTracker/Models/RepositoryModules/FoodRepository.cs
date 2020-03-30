using FoodTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public class FoodRepository : SubCategoryRepository, IFoodRepository
	{
		private readonly ApplicationDbContext context;

		public FoodRepository(ApplicationDbContext context) :base (context)
		{
			this.context = context;
		}
		public async Task<Food> AddFood(Food food)
		{
			context.Foods.Add(food);
			await context.SaveChangesAsync();
			return food;
		}

		public async Task<Food> DeleteFood(int id)
		{
			Food food = context.Foods.Find(id);
			if (food != null)
			{
				context.Foods.Remove(food);
				await context.SaveChangesAsync();
			}
			return food;
		}

		public async Task<IEnumerable<Food>> GetAllFood()
		{
			var foods = await context.Foods.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
			return foods;
		}

		public async Task<Food> GetFood(int? Id)
		{
			Food food = await context.Foods.Include(m => m.Category)
				.Include(m => m.SubCategory)
				.SingleOrDefaultAsync(m => m.ID == Id);
			return food;
		}

		public async Task<Food> UpdateFood(Food foodChanges)
		{
			var foodTypeFromDb = await context.Foods.FindAsync(foodChanges.ID);
			foodTypeFromDb.Name = foodChanges.Name;
			foodTypeFromDb.CategoryId = foodChanges.CategoryId;
			foodTypeFromDb.SubCategoryId = foodChanges.SubCategoryId;
			foodTypeFromDb.Measurement = foodChanges.Measurement;
			foodTypeFromDb.Priority = foodChanges.Priority;
			await context.SaveChangesAsync();
			return foodChanges;
		}
	}
}
