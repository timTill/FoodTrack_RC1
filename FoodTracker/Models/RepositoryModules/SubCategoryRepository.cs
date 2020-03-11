using FoodTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public class SubCategoryRepository : CategoryRepository, ISubCategoryRepository
	{
		private readonly ApplicationDbContext context;

		public SubCategoryRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}
		public async Task<SubCategory> GetSubCategory(int? id)
		{
			var subcategory = await context.SubCategory.SingleOrDefaultAsync(m => m.Id == id);
			return subcategory;
		}

		public async Task<IEnumerable<SubCategory>> GetSubCategoryByCategory(int id)
		{
			var SubCategories = await context.SubCategory.Where(s => s.CategoryId == id).ToListAsync();
			return SubCategories;
		}
		public IEnumerable<SubCategory> GetAllSubCategories()
		{
			var SubCategories = context.SubCategory.Include(s => s.Category).ToList();
			return SubCategories;
		}

		public async Task<List<string>> GetAllDistinctSubCategories()
		{
			var distinctSubCategories = await context.SubCategory
				.OrderBy(s => s.Name)
				.Select(s => s.Name).Distinct()
				.ToListAsync();
			return distinctSubCategories;
		}

		public async Task<SubCategory> AddSubCategory(SubCategory subcategory)
		{
			context.SubCategory.Add(subcategory);
			await context.SaveChangesAsync();
			return subcategory;
		}

		public async Task<bool> DoesSubCategoryExist(SubCategory subcategory)
		{
			var IdenticalSubCategory = await context.SubCategory.Include(s => s.Category).
				Where(s => s.Name == subcategory.Name && s.Category.Id == subcategory.CategoryId).ToListAsync();
			if (IdenticalSubCategory.Count > 0)
			{
				return true;
			}
			return false;
		}

		public async Task<SubCategory> UpdateSubCategory(SubCategory subcategory)
		{
			var subCatFromDb = await context.SubCategory.FindAsync(subcategory.Id);
			subCatFromDb.Name = subcategory.Name;
			await context.SaveChangesAsync();
			return subCatFromDb;
		}

		public async Task<SubCategory> DeleteSubCategory(int id)
		{
			var subCategoryToDel = await context.SubCategory
				.SingleOrDefaultAsync(s => s.Id == id);
			context.SubCategory.Remove(subCategoryToDel);
			await context.SaveChangesAsync();
			return subCategoryToDel;
		}
	}
}
