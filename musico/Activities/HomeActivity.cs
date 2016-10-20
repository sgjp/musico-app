using System;
using Android.App;
using Android.OS;
using Android.Views;

namespace musico
{
	[Activity (Label = "Musico")]
	public class HomeActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{

			RequestWindowFeature (WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Home);

		}

	}
}

