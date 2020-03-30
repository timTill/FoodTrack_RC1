using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Owner")]
	public class CategoryController : Controller
    {
		private readonly ICategoryRepository _repo;

		public CategoryController(ICategoryRepository repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public IActionResult Index()
		{			
			return View( _repo.GetAllCategories());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				await _repo.AddCategory(category);
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var category = await _repo.GetCategory(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				await _repo.UpdateCategory(category);

				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var category = await _repo.GetCategory(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			var category = await _repo.GetCategory(id);

			if (category == null)
			{
				return View();
			}
			await _repo.DeleteCategory(category);			
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{			
			if (id == null )
			{
				return RedirectToAction("ResourceNotFound", "Home", new { area = "Admin" });				
			}
			var category = await _repo.GetCategory(id);

			if (category == null)
			{
				return RedirectToAction("ResourceNotFound", "Home", new { area = "Admin" });				
			}
			return View(category);
		}
	}
}
