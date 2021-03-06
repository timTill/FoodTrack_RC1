﻿using FoodTracker.Models.RepositoryModules;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class FoodController : Controller
	{
		private readonly IUserFoodRepository _repo;

		[BindProperty]
		public FoodViewModel foodModel { get; set; }

		public string GetCurrentUserGUID()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			var userGUID = claim.Value;
			return userGUID;
		}


		public FoodController(IUserFoodRepository repo)
		{			
			this._repo = repo;

			foodModel = new FoodViewModel()
			{
				Category = _repo.GetAllCategories(),
				Food = new Models.Food()
			};
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string userGUID = GetCurrentUserGUID();
			var foods = await _repo.GetAllFoodTypeByOwner(userGUID);
			return View(foods);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View(foodModel);
		}

		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			SD.userGUID = claim.Value;
			foodModel.Food.OwnerName = SD.userGUID;
			ModelState.Remove("Food.OwnerName");
			if (!ModelState.IsValid)
			{
				return View(foodModel);
			}

			foodModel.Food.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
			await _repo.AddFood(foodModel.Food);
			return RedirectToAction(nameof(Index), "Home", new { area = "User" });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food = await _repo.GetFood(id);
			foodModel.SubCategory = await _repo.GetSubCategoryByCategory(foodModel.Food.CategoryId);
			if (foodModel.Food == null)
			{
				return NotFound();
			}
			return View(foodModel);
		}

		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPOST(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			foodModel.Food.CategoryId = Convert.ToInt32(Request.Form["CategoryId"].ToString());
			foodModel.Food.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
			foodModel.Food.ID = (int)id;
			await _repo.UpdateFood(foodModel.Food);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food = await _repo.GetFood(id);
			if (foodModel.Food == null)
			{
				return NotFound();
			}

			return View(foodModel);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food = await _repo.GetFood(id);

			if (foodModel.Food == null)
			{
				return NotFound();
			}

			return View(foodModel);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _repo.DeleteFood(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
