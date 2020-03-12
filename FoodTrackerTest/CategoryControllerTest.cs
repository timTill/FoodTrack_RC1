using FoodTracker;
using FoodTracker.Models;
using FoodTracker.Models.MockRepo;
using FoodTracker.Models.ViewModels;
using FoodTrackerTest.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTrackerTest
{
	public class CategoryControllerTest
	{
		public CategoryControllerTest()
		{
		
		}

		[Fact]
		public void TestMockGetAllCategories()
		{
			List<TestCategory> originalCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			MockCategRepo _repo = new MockCategRepo(originalCategoryList);						

			List<TestCategory> ExpectedCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			Assert.Equal(ExpectedCategoryList, _repo.GetAllCategories());
		}

		[Fact]
		public void TestMockGetCategory()
		{
			List<TestCategory> originalCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			MockCategRepo _repo = new MockCategRepo(originalCategoryList);

			TestCategory ExpectedCategory = new TestCategory { Id = 3, Name = "Elso" };
			Assert.Equal(ExpectedCategory, _repo.GetCategory(ExpectedCategory.Id));
		}


		[Fact]
		public void TestMockAddCategory()
		{
			List<TestCategory> originalCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			MockCategRepo _repo = new MockCategRepo(originalCategoryList);
			TestCategory toAdd = new TestCategory { Id = 8, Name = "Negyedik" };
			_repo.AddCategory(toAdd);
			List<TestCategory> ExpectedCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"},
					new TestCategory { Id = 8, Name = "Negyedik" }
				};

			Assert.Equal(ExpectedCategoryList, _repo.GetAllCategories());
		}

		[Fact]
		public void TestMockDeleteCategory()
		{
			List<TestCategory> originalCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			MockCategRepo _repo = new MockCategRepo(originalCategoryList);
			TestCategory toDelete = new TestCategory { Id = 7, Name = "Harmadik" };
			_repo.DeleteCategory(toDelete);

			List<TestCategory> ExpectedCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" }
				};
			Assert.Equal(ExpectedCategoryList, _repo.GetAllCategories());
		}

		[Fact]
		public void TestMockUpdateCategories()
		{
			List<TestCategory> originalCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			MockCategRepo _repo = new MockCategRepo(originalCategoryList);

			TestCategory toUpdate = new TestCategory { Id = 6, Name = "Otodik" };
			_repo.UpdateCategory(toUpdate);

			List<TestCategory> ExpectedCategoryList = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Otodik" },
					new TestCategory { Id= 7, Name="Harmadik"}
				};
			Assert.Equal(ExpectedCategoryList, _repo.GetAllCategories());
		}







	}
}
