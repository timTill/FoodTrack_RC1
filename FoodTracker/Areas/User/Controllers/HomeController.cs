using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodTracker.Controllers
{
	[Authorize]
	[Area("User")]
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly IUserFoodRepository _repo;

		public HomeController(ApplicationDbContext db, IUserFoodRepository repo)
		{
			_db = db;
			this._repo = repo;
		}

		public string GetCurrentUserGUID()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			var userGUID = claim.Value;
			return userGUID;
		}

		public async Task<IActionResult> Index()
		{
			string UserGUID = GetCurrentUserGUID();
			IndexViewModel IndexVM = new IndexViewModel()
			{
				FoodItem = await _repo.GetAllRealFoodByOwner(UserGUID),
				Category = _repo.GetAllCategories(),//_db.Category.ToListAsync(),
			};
			return View(IndexVM);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Nullify(int? id)
		{
			if (id != null)
			{
				Food foodToNull = await _repo.GetFood(id);//_db.Foods.FindAsync(id);
				foodToNull.BestBefore = null;
				foodToNull.QuantityLeft = 0;
				foodToNull.Description = String.Empty;
				foodToNull.Unit = 0;
				_repo.SaveDB(); //_db.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> RemoveFromStockConf(int? id)
		{
			if (id != null)
			{
				Food foodToConfirm = await _repo.GetFood(id);
					//await _db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				if (foodToConfirm != null)
				{
					return View(foodToConfirm);
				}				
			}
			return NotFound();
		}

		public async Task<IActionResult> UpdateStock(int? id)
		{
			Food food = await _repo.GetFood(id);
			//_db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
			if (food == null)
				return NotFound();
			return View(food);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateStock(Food food)
		{
			if (food != null)
			{
				Food newFood = await _repo.GetFood(food.ID);
				//_db.Foods.Where(f => f.ID == food.ID).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				newFood.Description = food.Description;
				newFood.BestBefore = food.BestBefore;
				newFood.QuantityLeft = food.QuantityLeft;
				newFood.Unit = food.Unit;
				_repo.SaveDB(); //_db.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
