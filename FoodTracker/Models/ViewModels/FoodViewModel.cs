using System.Collections.Generic;

namespace FoodTracker.Models.ViewModels
{
	public class FoodViewModel
	{
		public Food Food{ get; set; }
		public IEnumerable<Category> Category { get; set; }
		public IEnumerable<SubCategory> SubCategory { get; set; }
	}
}
