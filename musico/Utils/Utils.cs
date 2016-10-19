using System;
using Android.Net;
using Android.Content;

namespace musico
{
	public static class Utils
	{
		

		public static bool isNetworkAvailable (Context context)
		{   
			ConnectivityManager cm = (ConnectivityManager)context   
				.GetSystemService (Context.ConnectivityService);   
			if (cm == null) {   
			} else {
				NetworkInfo[] info = cm.GetAllNetworkInfo ();   
				if (info != null) {   
					for (int i = 0; i < info.Length; i++) {   
						if (info [i].GetState () == NetworkInfo.State.Connected) {   
							return true;   
						}   
					}   
				}   
			}   
			return false;   
		}
	}
}

