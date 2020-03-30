using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult ResourceNotFound()
		{
			Response.StatusCode = 404;
			return View("ResourceNotFound"); //NotFound();
		}
	}
}