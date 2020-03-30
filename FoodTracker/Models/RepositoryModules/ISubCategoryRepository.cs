using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public interface ISubCategoryRepository :ICategoryRepository
	{
		Task<SubCategory> GetSubCategory(int? id);
		IEnumerable<SubCategory> GetAllSubCategories();
		Task<IEnumerable<SubCategory>> GetSubCategoryByCategory(int id);
		Task<SubCategory> AddSubCategory(SubCategory subcategory);
		Task<SubCategory> UpdateSubCategory(SubCategory subcategory);
		Task<List<string>> GetAllDistinctSubCategories();
		Task<bool> DoesSubCategoryExist(SubCategory subcategory);
		Task<SubCategory> DeleteSubCategory(int id);
	}
}
