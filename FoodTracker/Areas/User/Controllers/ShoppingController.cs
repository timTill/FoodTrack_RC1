using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodTracker.Areas.User.Controllers
{
	[Authorize]
	[Area("User")]
	public class ShoppingController : Controller
    {
		
		private readonly IUserFoodRepository _repo;

		public ShoppingController (IUserFoodRepository repo)
		{
			this._repo = repo;
		}

		public string GetCurrentUserGUID()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			var userGUID = claim.Value;
			return userGUID;
		}
		
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string userGUID = GetCurrentUserGUID();
			IndexViewModel IndexVM = new IndexViewModel()
			{			
				FoodItem =await _repo.GetAllFoodTypeByOwner(userGUID),
				/*FoodItem = await _db.Foods.Include(m => m.Category).
						Include(m => m.SubCategory).Where(f=>f.OwnerName == userGUID).ToListAsync(),*/
				Category = _repo.GetAllCategories(),
			};
			return View(IndexVM);
		}

		public async Task<IActionResult> AddToStock(int id)
		{
			Food selectedFood = await _repo.GetFood(id);
			if (selectedFood == null)
				return NotFound();
			return View(selectedFood);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("AddToStock")]
		public async Task<IActionResult> AddToStockPost(Food food)
		{
			Food selectedFood = await _repo.GetFood(food.ID);
			selectedFood.BestBefore = food.BestBefore;
			selectedFood.Description = food.Description;
			selectedFood.QuantityLeft = food.QuantityLeft;
			selectedFood.Unit = food.Unit;
			selectedFood.IsInCart = false; //cart is ezt a fv-t használja, h onnan kikerüljön

			if (food.BestBefore != null && food.QuantityLeft != 0)
			{
				_repo.SaveDB();
				return RedirectToAction("Index","Home");
			}
			return View(selectedFood);
		}

		public async Task<IActionResult> AddToCart(int id)
		{
			Food foodToCart = await _repo.GetFood(id);
			if (foodToCart != null)
			{
				foodToCart.IsInCart = true;
				_repo.SaveDB();
			}
			else return NotFound();
			return RedirectToAction(nameof(Index));
		}
	}
}