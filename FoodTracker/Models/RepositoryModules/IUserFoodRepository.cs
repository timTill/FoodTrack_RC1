using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.RepositoryModules
{
	public interface IUserFoodRepository : IFoodRepository
	{
		Task<IEnumerable<Food>> GetAllFoodTypeByOwner(string userGUID);
		Task<IEnumerable<Food>> GetAllRealFoodByOwner(string userGUID);
		Task<IEnumerable<Food>> GetAllFoodInCartByOwner(string userGUID);
	}
}
