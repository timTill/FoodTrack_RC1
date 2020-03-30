using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class SubCategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly ISubCategoryRepository _repo;

		[TempData]
		public string StatusMessage { get; set; }

		public SubCategoryController(ApplicationDbContext db, ISubCategoryRepository repo)
		{
			_db = db;
			this._repo = repo;
		}

		public IActionResult Index()
		{
			var subCategories =  _repo.GetAllSubCategories();		
			return View(subCategories);
		}

		
		[HttpGet]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> Create()
		{
			SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
			{
				CategoryList =  _repo.GetAllCategories(),
				SubCategory = new Models.SubCategory(),
				SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),								
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
		{
			if (ModelState.IsValid)
			{			
				if (await _repo.DoesSubCategoryExist(model.SubCategory))
				{
					//Error
					StatusMessage = "Error : Sub Category already exists. Please use another name.";
				}
				else
				{
					await _repo.AddSubCategory(model.SubCategory);
					return RedirectToAction(nameof(Index));
				}
			}
			SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
			{
				CategoryList = _repo.GetAllCategories(),
				SubCategory = model.SubCategory,
				SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
				StatusMessage = StatusMessage
			};
			return View(modelVM);
		}

		[Authorize]		
		public async Task<IActionResult> GetSubCategory(int id)
		{
			IEnumerable<SubCategory> subCategories = await _repo.GetSubCategoryByCategory(id);
			return Json(new SelectList(subCategories, "Id", "Name"));
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var subCategory = await _repo.GetSubCategory(id);
			if (subCategory == null)
			{
				return NotFound();
			}

			SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
			{
				CategoryList = _repo.GetAllCategories(),
				SubCategory = subCategory,
				SubCategoryList = await _repo.GetAllDistinctSubCategories()
			};
			return View(model);
		}

		//POST - EDIT
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> Edit(SubCategoryAndCategoryViewModel model)
		{
			if (ModelState.IsValid)
			{
				var doesSubCategoryExists = await _repo.DoesSubCategoryExist(model.SubCategory);

				if (doesSubCategoryExists)
				{
					//Error
					StatusMessage = "Error : Sub Category exists. Please use another name.";
				}
				else
				{
					await _repo.UpdateSubCategory(model.SubCategory);
					return RedirectToAction(nameof(Index));
				}
			}
			SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
			{
				CategoryList =  _repo.GetAllCategories(),
				SubCategory = model.SubCategory,
				SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
				StatusMessage = StatusMessage
			};
			return View(modelVM);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var subCategory = await _db.SubCategory.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
			if (subCategory == null)
			{
				return NotFound();
			}
			return View(subCategory);
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var subCategory = await _db.SubCategory.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
			if (subCategory == null)
			{
				return NotFound();
			}

			return View(subCategory);
		}

		[ValidateAntiForgeryToken]
		[HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin,Owner")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _repo.DeleteSubCategory(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
