using System.Collections.Generic;

namespace FoodTracker.Models.ViewModels
{
	public class PortDBViewModel
	{
		public IEnumerable<Category> Categories { get; set; }
		//public List<Category> Categories { get; set; }
		public IEnumerable<SubCategory> Subcategories { get; set; }
		public IEnumerable<Food> Foods { get; set; }
	}
}
