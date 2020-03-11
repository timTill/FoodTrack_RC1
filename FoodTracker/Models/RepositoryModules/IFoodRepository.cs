using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public interface IFoodRepository :ISubCategoryRepository
	{
		Task<Food> GetFood(int? Id);
		Task<IEnumerable<Food>> GetAllFood();
		Task<Food> AddFood(Food food);
		Task<Food> UpdateFood(Food FoodChanges);
		Task<Food> DeleteFood(int id);
	}
}
