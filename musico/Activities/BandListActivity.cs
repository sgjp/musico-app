
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace musico
{
	[Activity (Label = "Musico")]			
	public class BandListActivity : Activity
	{
		ListView bandListTV;

		string[] names ;
		float[] prices ;
		float[] ratings ;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature (WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.BandList);

			initBandList ();
		}

		private void initBandList(){
			names = this.Intent.GetStringArrayExtra("names");
			prices = this.Intent.GetFloatArrayExtra("prices");
			ratings = this.Intent.GetFloatArrayExtra("ratings");

			bandListTV = FindViewById<ListView> (Resource.Id.BandList);

			CustomBandListAdapter listAdapter = new CustomBandListAdapter (this, names, prices, ratings);

			bandListTV.SetAdapter (listAdapter);

			bandListTV.ItemClick += BandListTV_ItemClick;

		}

		void BandListTV_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			Intent intent = new Intent (this, typeof(DetailActivity));
			intent.PutExtra ("name", bandListTV.GetItemAtPosition(e.Position).ToString());
			StartActivity (intent);
		}
	}
}

