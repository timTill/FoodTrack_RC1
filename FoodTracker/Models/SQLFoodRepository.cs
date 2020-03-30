namespace FoodTracker.Models
{ }/*
	public class SQLRepository : ISQLRepository
	{
		private readonly ApplicationDbContext context;
		private readonly ICategoryRepository categoryRepo;

		public SQLRepository(ApplicationDbContext context, ICategoryRepository categoryRepo)
		{
			this.context = context;
			this.categoryRepo = categoryRepo;
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

		//SubCategories
		public async Task<SubCategory> GetSubCategory(int? id)
		{
			var subcategory = await context.SubCategory.SingleOrDefaultAsync(m => m.Id == id);
			return subcategory;

		}

		public async Task<IEnumerable<SubCategory>> GetSubCategoryByCategory(int id)
		{
			var SubCategories = await context.SubCategory.Where(s => s.CategoryId == id)
									.ToListAsync();
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
				.Select(s =>s.Name).Distinct()
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
			if (IdenticalSubCategory.Count>0)
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

		//Categories
		public IEnumerable<Category> GetAllCategories()
		{
			IEnumerable<Category> Categories =  context.Category;
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
			if (id!=null)
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

	}
}



*/