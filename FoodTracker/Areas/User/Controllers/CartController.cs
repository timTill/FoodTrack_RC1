using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Areas.User.Controllers
{
	[Area("User")]
	public class CartController : Controller
    {
		private readonly ApplicationDbContext _db;
		public CartController(ApplicationDbContext db)
		{
			_db = db;
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
				//FoodItem = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
				FoodItem = await _db.Foods.Where(m => m.IsInCart== true).Include(m => m.Category).
				Include(m => m.SubCategory).Where(f => f.OwnerName == userGUID).ToListAsync(),
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
			//Food selectedFood = _db.Foods.Where(f => f.ID == id).Include(m => m.Category).Include(m => m.SubCategory).FirstOrDefault();
			else return BadRequest("Request error on removing fooditem from cart.");
			return RedirectToAction(nameof(Index));

		}
	}
}