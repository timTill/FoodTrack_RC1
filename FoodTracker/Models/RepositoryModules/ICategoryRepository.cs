using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> GetAllCategories();
		Task<Category> AddCategory(Category category);
		Task<Category> GetCategory(int? id);
		Task<Category> UpdateCategory(Category category);
		Task<Category> DeleteCategory(Category category);
		void SaveDB();
	}
}
