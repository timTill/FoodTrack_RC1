using Microsoft.AspNetCore.Identity;

namespace FoodTracker.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
