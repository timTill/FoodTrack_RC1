using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using Microsoft.AspNetCore.Mvc;
using FoodTracker.Extensions;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using System.Security.Claims;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class DataController : Controller
    {
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IDataManager ImpExpMan;


		//public DataController(ApplicationDbContext applicationDbContext)
		//public DataController(ApplicationDbContext applicationDbContext, ImportExportManager iem)
		public DataController(ApplicationDbContext applicationDbContext, IDataManager ImpExpMan)
		{
			
			this.applicationDbContext = applicationDbContext;
			this.ImpExpMan = ImpExpMan;
		}

		public IActionResult Export()
		{
			List<Category> CategoryItems = applicationDbContext.Category.ToList();
			IEnumerable<SubCategory> SubCategoryItems = applicationDbContext.SubCategory.ToList();
			IEnumerable<Food> FoodItems = applicationDbContext.Foods.ToList();
			PortDBViewModel DB_VM = new PortDBViewModel
			{
				Categories = CategoryItems,
				Subcategories = SubCategoryItems,
				Foods = FoodItems
			};
			ImpExpMan.ExportXML(DB_VM);
			return RedirectToAction("Index", "Home", new { area = "User" });		
		}

		public IActionResult Import()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			SD.userGUID = claim.Value;
			ImpExpMan.ImportXML();
			return RedirectToAction("Index", "Home", new { area = "User" });
		}
		public IActionResult Index()
        {
            return View();
        }
    }
}