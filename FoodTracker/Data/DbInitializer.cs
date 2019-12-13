using FoodTracker.Models;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Data
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _usermanager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> roleManager)
		{
			this._db = db;
			this._usermanager = usermanager;
			this._roleManager = roleManager;
		}
		public async void Initialize()
		{
			try
			{
				if (this._db.Database.GetPendingMigrations().Count()>0)
				{
					this._db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				
			}

			if (_db.Roles.Any(r => r.Name == SD.OwnerUser)) return;

			_roleManager.CreateAsync(new IdentityRole(SD.OwnerUser)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.AdminUser)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.EndUser)).GetAwaiter().GetResult();

			_usermanager.CreateAsync(new ApplicationUser
			{
				UserName = SD.ownerEmail,
				Email = SD.ownerEmail,
				Name = "Teszt Elek",
				EmailConfirmed = true
			}, SD.ownerpw).GetAwaiter().GetResult();


			_usermanager.CreateAsync(new ApplicationUser
			{
				UserName = SD.endUserEmail,
				Email = SD.endUserEmail,
				Name = "Teszt János",
				EmailConfirmed = true
			}, SD.ownerpw).GetAwaiter().GetResult();

			IdentityUser ownerUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == SD.ownerEmail);
			IdentityUser endUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == SD.endUserEmail);
			await _usermanager.AddToRoleAsync(ownerUser, SD.OwnerUser);
			await _usermanager.AddToRoleAsync(endUser, SD.EndUser);
		}
	}
}