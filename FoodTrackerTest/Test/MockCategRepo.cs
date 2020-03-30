using FoodTrackerTest.Test;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTracker.Models.MockRepo
{
	public class MockCategRepo
	{
		private List<TestCategory> _mockRepo;

		public MockCategRepo(List<TestCategory> MockRepo)
		{
			_mockRepo = MockRepo;
		}

		public Task<Category> AddCategory(TestCategory category)
		{
			_mockRepo.Add(category);
			return null;
		}

		public Category DeleteCategory(TestCategory category)
		{
			_mockRepo.Remove(category);
			return category;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _mockRepo;
		}

		public Category GetCategory(int? id)
		{
			if (id != null)
			{
				return _mockRepo.Find(m => m.Id == id);
			}

			return null;
		}

		public Task<Category> UpdateCategory(TestCategory category)
		{
			TestCategory toUpdate = _mockRepo.Find(m => m.Id == category.Id);
			toUpdate.Name = category.Name;
			return null;
		}
	}
}
