﻿using FoodTracker.Models;

namespace FoodTrackerTest.Test
{
	public class TestCategory : Category
	{
		public override bool Equals(object obj)
		{
			TestCategory catparam = (TestCategory)obj;
			return ((this.Name == catparam.Name) && (this.Id == catparam.Id));
		}
	}
}
