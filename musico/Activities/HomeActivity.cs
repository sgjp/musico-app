using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Musico;
using System.Collections.Generic;

namespace musico
{
	[Activity (Label = "Musico")]
	public class HomeActivity : Activity
	{
		private Button homeButton;
		private Button searchButton;

		private ListView recomendedListView;

		private IList<Band> recommendedBandList;

		List<String> listItems=new List<String>();

		ArrayAdapter<String> adapter;



		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature (WindowFeatures.NoTitle);	
			homeButton = FindViewById<Button> (Resource.Id.homeButton);
			searchButton = FindViewById<Button> (Resource.Id.searchButton);



			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Home);

			initBandRecommendation ();


		}

		private void initBandRecommendation(){
			
			recomendedListView = FindViewById<ListView> (Resource.Id.reccomendedListView);
			recommendedBandList = MusicoConnUtil.SearchBandsAsync ("saskatoon", null, 0, 9999999, 0, new DateTime ());

			string[] items = new string[recommendedBandList.Count];
			for (int i=0; i<recommendedBandList.Count; i++){
				items [i] = recommendedBandList [i].Name;
			}


			//ArrayAdapter<String> listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);

	
			CustomListAdapter listAdapter = new CustomListAdapter(this,items);



			recomendedListView.SetAdapter (listAdapter);

		
		}



	}
}

