using Android.App;
using Android.Widget;
using Android.OS;
using Musico;
using System.Collections.Generic;
using System;

namespace musico
{
	[Activity (Label = "musico", Icon = "@mipmap/logosmall")]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//test ();

			/*
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
			*/
		}


	}
}


