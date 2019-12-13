using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Areas.User.Controllers
{
	[Area("User")]
	public class ShoppingController : Controller
    {
		private readonly ApplicationDbContext _db;
		public ShoppingController(ApplicationDbContext db)
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

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string userGUID = GetCurrentUserGUID();
			IndexViewModel IndexVM = new IndexViewModel()
			{
				//FoodItem = await _db.Foods.Where(m => m.QuantityLeft == 0).Include(m => m.Category).
				//Include(m => m.SubCategory).ToListAsync(),
				//Category = await _db.Category.ToListAsync(),
				FoodItem = await _db.Foods.Include(m => m.Category).
						Include(m => m.SubCategory).Where(f=>f.OwnerName == userGUID).ToListAsync(),
				Category = await _db.Category.ToListAsync(),
			};
			return View(IndexVM);
		}
		public IActionResult AddToStock(int id)
		{			
			Food selectedFood = _db.Foods.Where(f => f.ID == id).Include(m => m.Category).Include(m => m.SubCategory).FirstOrDefault();
			return View(selectedFood);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("AddToStock")]
		public async Task<IActionResult> AddToStockPost(Food food)
		{
			Food selectedFood = _db.Foods.Where(f => f.ID == food.ID).Include(m => m.Category).Include(m => m.SubCategory).FirstOrDefault();
			selectedFood.BestBefore = food.BestBefore;
			selectedFood.Description = food.Description;
			selectedFood.QuantityLeft = food.QuantityLeft;
			selectedFood.Unit = food.Unit;
			selectedFood.IsInCart = false; //cart is ezt a fv-t használja, h onnan kikerüljön

			if (food.BestBefore != null && food.QuantityLeft != 0)
			{			
				await _db.SaveChangesAsync();
				return RedirectToAction("Index","Home");
			}
			return View(selectedFood);
		}

		public async Task<IActionResult> AddToCart(int id)
		{
			Food foodToCart = await _db.Foods.FirstOrDefaultAsync(f => f.ID == id);
			if (foodToCart != null)
			{
				foodToCart.IsInCart = true;
				await _db.SaveChangesAsync();
			}
			//Food selectedFood = _db.Foods.Where(f => f.ID == id).Include(m => m.Category).Include(m => m.SubCategory).FirstOrDefault();
			else return BadRequest("Request error on putting fooditem to cart.");
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> DeleteFoodTypeConf(int? id)
		{
			if (id != null)
			{
				Food foodToConfirm = await _db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				return View(foodToConfirm);
			}
			return RedirectToAction(nameof(Index));
		}
		
		public async Task<IActionResult> DeleteFoodType(int? id)
		{
			if (id != null)
			{
				Food foodToDelete = await _db.Foods.FindAsync(id);
				_db.Foods.Remove(foodToDelete);				
				await _db.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}