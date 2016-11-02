using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Musico;
using System.Collections.Generic;
using Android.Content;

namespace musico
{
	[Activity (Label = "Musico")]
	public class HomeActivity : Activity
	{


		private ListView recomendedListView;
		private ListView topBandsListView;
		private ListView topUsersListView;

		private Button searchButton;

		private IList<Band> recommendedBandList;
		private List<Band> topBandsList;
		private List<TopUser> topUsersList;

		List<String> listItems=new List<String>();

		ArrayAdapter<String> adapter;



		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature (WindowFeatures.NoTitle);	
			//homeButton = FindViewById<Button> (Resource.Id.homeButton);
			//searchButton = FindViewById<Button> (Resource.Id.searchButton);



			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Home);

			searchButton = FindViewById<Button> (Resource.Id.btn_search);

			searchButton.Click += SearchButton_Click;

			initBandRecommendation ();
			initTopBands ();
			initTopUsers ();


		}

		void SearchButton_Click (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof(SearchActivity));
			StartActivity (intent);
		}

		private void initTopUsers(){
			topUsersListView = FindViewById<ListView> (Resource.Id.TopUsersListView);

			topUsersList = MusicoConnUtil.GetTopUsers ();



			int maxUsers = 2 ;
			if (topUsersList.Count <= 2)
				maxUsers = topUsersList.Count-1;

			string[] items = new string[maxUsers+1];

			int startIndex = 0;
			for (int i=maxUsers; i>=0; i--){
				items [startIndex] = topUsersList [i].UserName;
				startIndex++;
			}
				
			CustomListAdapter listAdapter = new CustomListAdapter(this,items);
			topUsersListView.SetAdapter (listAdapter);
			topUsersListView.Clickable = false;

		}

		private void initTopBands(){
			topBandsListView = FindViewById<ListView> (Resource.Id.TopListView);
			topBandsList = MusicoConnUtil.GetAllBandsAsync ();

			topBandsList.Sort ();



			int maxBands = 4;
			if (topBandsList.Count <= 4)
				maxBands = topBandsList.Count-1;


			string[] items = new string[maxBands+1];
			int startIndex = 0;
			for (int i=maxBands; i>=0; i--){
				items [startIndex] = topBandsList [i].Name;
				startIndex++;
			}

			CustomListAdapter listAdapter = new CustomListAdapter(this,items);
			topBandsListView.SetAdapter (listAdapter);
			topBandsListView.Clickable = true;

			topBandsListView.ItemClick += topBandClick_Click;

		}

		void topBandClick_Click (object sender, AdapterView.ItemClickEventArgs e)
		{
			Intent intent = new Intent (this, typeof(DetailActivity));
			intent.PutExtra ("name", topBandsListView.GetItemAtPosition (e.Position).ToString());
			StartActivity (intent);

		}

		private void initBandRecommendation(){
			
			recomendedListView = FindViewById<ListView> (Resource.Id.reccomendedListView);
			recommendedBandList = MusicoConnUtil.SearchBandsAsync ("saskatoon", null, 0, 9999999, 0, new DateTime (),null);



			int maxBands = 5;
			if (recommendedBandList.Count < 5)
				maxBands = recommendedBandList.Count;

			string[] items = new string[maxBands];
			for (int i=0; i<maxBands; i++){
				items [i] = recommendedBandList [i].Name;
			}

			//ArrayAdapter<String> listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);

	
			CustomListAdapter listAdapter = new CustomListAdapter(this,items);


			recomendedListView.SetAdapter (listAdapter);
			recomendedListView.Clickable = true;
			recomendedListView.ItemClick += RecomendedListView_ItemClick;
		}

		void RecomendedListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			Intent intent = new Intent (this, typeof(DetailActivity));
			intent.PutExtra ("name", recomendedListView.GetItemAtPosition (e.Position).ToString());
			StartActivity (intent);
		}



	}
}

