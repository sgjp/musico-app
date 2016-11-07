using System;

namespace Musico
{
	public static class Globals
	{
		//Base
		public static string BASE_URL = "http://10.81.4.87:8080";
		//public static string BASE_URL = "https://musico-js.herokuapp.com";

		//Bands
		public static string BANDS = BASE_URL + "/bands";

		public static string BANDS_SEARCH = BASE_URL + "/bands/search";


		//Band

		public static string BAND = BASE_URL + "/band";

		public static string BAND_BY_NAME = BAND + "/name";

		public static string BAND_REVIEW = "/review";

		public static string BAND_COMMENT = "/comment";

		public static string BAND_BOOKING = "/booking";

		//User

		public static string AUTH = BASE_URL + "/auth/login";

		public static string TOP_USERS = BASE_URL + "/user/top";
	}
}

