﻿using FoodTracker.Data;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles =SD.OwnerUser)]
    public class UserController : Controller
    {
		private readonly ApplicationDbContext _db;

		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
        {
			var claimsIdentity = (ClaimsIdentity)this.User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			return View(await _db.ApplicationUser.Where(u=>u.Id!=claim.Value).ToListAsync());
        }
    }
}