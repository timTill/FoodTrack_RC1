using System.Collections.Generic;

namespace FoodTracker.Utility
{
	public static class SD
	{
		public const string OwnerUser = "Owner";
		public const string AdminUser = "Admin";		
		public const string EndUser = "EndUser";
		public const string XMLinPath = @"C:\Users\Szabolcs\Source\Repos\FoodTracker\FoodTracker\FoodTracker\Port\DataToImport.xml";
		public const string ownerEmail = "ownerFT@gmail.com";
		public const string ownerpw = "Tesztellek1*";
		public const string adminName = "Migrator";
		public const string adminEmail = "adminka@gmail.com";		
		public const string adminpw = "Adminollak1*";
		public const string endUserEmail = "enduser@gmail.com";
		public const string endUserPw = "Tesztellek1*";		
		public static string userGUID;
		public enum ENVS {BaseLine, BCK, DEV_VS, DEV_LOCIIS, UAT, Prod};		
		public static List<string> RNList = new List<string>()
		{ "1_2_1","1_2", "1_1_5", "1_1" };

		public static string appVersion = RNList[0].Replace('_','.')+'.';
	}
}
