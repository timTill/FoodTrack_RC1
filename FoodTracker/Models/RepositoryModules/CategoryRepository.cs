using FoodTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext context;

		public CategoryRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			IEnumerable<Category> Categories = context.Category;
			return Categories;
		}

		public async Task<Category> AddCategory(Category category)
		{
			context.Category.Add(category);
			await context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> GetCategory(int? id)
		{
			if (id != null)
			{
				var category = await context.Category.FindAsync(id);
				return category;
			}
			return null;
		}

		public async Task<Category> UpdateCategory(Category category)
		{
			context.Update(category);
			await context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> DeleteCategory(Category category)
		{
			context.Category.Remove(category);
			await context.SaveChangesAsync();
			return category;
		}

		public void SaveDB()
		{
			context.SaveChanges();
		}
	}
}
