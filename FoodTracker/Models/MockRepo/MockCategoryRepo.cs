//obsolete?
using FoodTracker.Models.RepositoryModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.MockRepo
{
	public class MockCategoryRepo : ICategoryRepository
	{
		private List<Category> _mockRepo;

		public MockCategoryRepo(List<Category> MockRepo)
		{
			_mockRepo = MockRepo;	
		}

		public Task<Category> AddCategory(Category category)
		{
			_mockRepo.Add(category);
			return null;
		}

		public Task<Category> DeleteCategory(Category category)
		{
			_mockRepo.Remove(category);
			return null;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _mockRepo;
		}

		public Task<Category> GetCategory(int? id)
		{
			return null;
		}

		public Category GetCategorySync(int? id)
		{
			if (id != null)
			{
				return _mockRepo[int.Parse(id.ToString())];
			}

			return null;
		}

		public void SaveDB()
		{
			return;
		}

		public Task<Category> UpdateCategory(Category category)
		{
			/*
			Category CategoryToUpdate = GetCategorySync(category.Id);
			CategoryToUpdate.Name = category.Name;
			*/
			_mockRepo[category.Id].Name = category.Name;
			return null;
			
		}
	}
}
