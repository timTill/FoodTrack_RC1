using System.Collections.Generic;

namespace FoodTracker.Models.ViewModels
{
	public class FoodStockCreate
	{
		public Food Food { get; set; }
		IEnumerable<QuantityLeft> quantityLeft { get; set; }
	}
}
