using System.Collections.Generic;

namespace FoodTracker.Models.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<Food> FoodItem { get; set; }
		public IEnumerable<Category> Category { get; set; }			
	}
}
