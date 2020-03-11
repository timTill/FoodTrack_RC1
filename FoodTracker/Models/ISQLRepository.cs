







using FoodTracker.Models.RepositoryModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
	public interface ISQLRepository : ICategoryRepository, ISubCategoryRepository, IFoodRepository

	{
		/*
		Task<Food> GetFood(int? Id);
		Task<IEnumerable<Food>> GetAllFood();
		Task<Food> AddFood(Food food);
		Task<Food> UpdateFood(Food FoodChanges);
		Task<Food> DeleteFood(int id);

		Task<SubCategory> GetSubCategory(int? id);
		IEnumerable<SubCategory> GetAllSubCategories();
		Task<IEnumerable<SubCategory>> GetSubCategoryByCategory(int id);
		Task<SubCategory> AddSubCategory(SubCategory subcategory);
		Task<SubCategory> UpdateSubCategory(SubCategory subcategory);
		Task<List<string>> GetAllDistinctSubCategories();
		Task<bool> DoesSubCategoryExist(SubCategory subcategory);
		Task<SubCategory> DeleteSubCategory(int id);

		IEnumerable<Category> GetAllCategories();
		Task<Category> AddCategory(Category category);
		Task<Category> GetCategory(int? id);
		Task<Category> UpdateCategory(Category category);
		Task<Category> DeleteCategory(Category category);
		*/
	}
}

