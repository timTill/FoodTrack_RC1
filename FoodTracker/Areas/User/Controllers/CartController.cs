using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.RepositoryModules;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodTracker.Areas.User.Controllers
{
	[Authorize]
	[Area("User")]
	public class CartController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly IUserFoodRepository _repo;

		public CartController(ApplicationDbContext db, IUserFoodRepository repo)
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
			string userGUID = GetCurrentUserGUID();
			IndexViewModel IndexVM = new IndexViewModel()
			{
				FoodItem = await _repo.GetAllFoodInCartByOwner(userGUID),
				/*FoodItem = await _db.Foods.Where(m => m.IsInCart== true).Include(m => m.Category).
				Include(m => m.SubCategory).Where(f => f.OwnerName == userGUID).ToListAsync(),*/
				Category = await _db.Category.ToListAsync(),
			};
			return View(IndexVM);
		}

		public async Task<IActionResult> RemoveFromCart(int id)
		{
			Food foodToCart = await _db.Foods.FirstOrDefaultAsync(f => f.ID == id);
			if (foodToCart != null)
			{
				foodToCart.IsInCart = false;
				await _db.SaveChangesAsync();
			}
			//else return BadRequest("Request error on removing fooditem from cart.");
			else return NotFound();
			return RedirectToAction(nameof(Index));
		}
	}
}