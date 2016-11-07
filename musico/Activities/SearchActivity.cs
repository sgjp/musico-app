
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
using Java.Sql;
using Musico;

namespace musico
{
	[Activity (Label = "Search")]			
	public class SearchActivity : Activity
	{
		private SeekBar minPriceSeekBar;
		private SeekBar maxPriceSeekBar;
		private TextView minPriceValue;
		private TextView maxPriceValue;

		private TextView nameTV;
		private TextView locationTV;
		private TextView genreTV;
		private TextView dateTV;

		private Button searchBTN;

		private RatingBar minRatingBar;

		private string name;
		private string location;
		private string genre;
		private int minPrice;
		private int maxPrice;
		private float minRating;
		private string date;

		private string userId;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			userId = this.Intent.GetStringExtra ("id");
			RequestWindowFeature (WindowFeatures.NoTitle);	



			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Search);

			initComponents ();
			initPriceSeekBars ();
		}

		private void initComponents(){
			nameTV = FindViewById<TextView> (Resource.Id.Name);
			genreTV = FindViewById<TextView> (Resource.Id.Genre);
			locationTV = FindViewById<TextView> (Resource.Id.Location);
			dateTV = FindViewById<TextView> (Resource.Id.DateSearch);
			searchBTN = FindViewById<Button> (Resource.Id.btn_search);
			minRatingBar = FindViewById<RatingBar> (Resource.Id.Rating);

			searchBTN.Click += SearchBTN_Click;
		
		}

		void SearchBTN_Click (object sender, EventArgs e)
		{
			List<Band> foundBands;

			name = null;

			if (nameTV.Text.Length>0)
				name = nameTV.Text;

			genre = null;

			if( genreTV.Text.Length>0)
				genre = genreTV.Text;

			location = null;

			if (locationTV.Text.Length >0)
				location = locationTV.Text;

			date = null;

			if(dateTV.Text.Length==10)
				date = dateTV.Text;

			minRating = 0;

			if (minRatingBar.Rating>0.0)
				minRating = minRatingBar.Rating;

			int minRatingI = (int)Math.Round(minRating, 0);

			minPrice = minPriceSeekBar.Progress;

			maxPrice = 0;

			if (maxPriceSeekBar.Progress>0)
				maxPrice = maxPriceSeekBar.Progress;

			foundBands = MusicoConnUtil.SearchBandsAsync (location, genre, minPrice, maxPrice, minRatingI, Convert.ToDateTime (date), name);
			foundBands.Sort();

			string[] names = new string[foundBands.Count];
			float[] prices = new float[foundBands.Count];
			float[] ratings = new float[foundBands.Count];

			int j = foundBands.Count - 1;
			for (int i=0 ; i<foundBands.Count ; i++){
				names [j] = foundBands [i].Name;
				prices [j] = foundBands [i].AvgPrice;
				ratings [j] = foundBands [i].AvgRate;
				j--;
			}

			Intent intent = new Intent (this, typeof(BandListActivity));

			intent.PutExtra ("names", names);
			intent.PutExtra ("prices", prices);
			intent.PutExtra ("ratings", ratings);
			intent.PutExtra ("id", userId);
			StartActivity (intent);


		}
		private void initPriceSeekBars(){
			minPriceSeekBar = FindViewById<SeekBar> (Resource.Id.minPriceSeekBar);
			maxPriceSeekBar = FindViewById<SeekBar> (Resource.Id.maxPriceSeekBar);
		
			minPriceValue = FindViewById<TextView> (Resource.Id.MinPriceValue);
			maxPriceValue = FindViewById<TextView> (Resource.Id.MaxPriceValue);

			minPriceSeekBar.Touch += (s, e) =>
			{
				var handled = false;
				if (e.Event.Action == MotionEventActions.Down)
				{
					minPriceValue.Text = "$"+minPriceSeekBar.Progress;

					//handled = true;
				}
				else if (e.Event.Action == MotionEventActions.Move)
				{
					minPriceValue.Text = "$"+minPriceSeekBar.Progress;
					//handled = true;
				}

				e.Handled = handled;
			};

			maxPriceSeekBar.Touch += (s, e) =>
			{
				var handled = false;
				if (e.Event.Action == MotionEventActions.Down)
				{
					maxPriceValue.Text = "$"+maxPriceSeekBar.Progress;

					//handled = true;
				}
				else if (e.Event.Action == MotionEventActions.Move)
				{
					maxPriceValue.Text = "$"+maxPriceSeekBar.Progress;
					//handled = true;
				}

				e.Handled = handled;
			};





		}


	}
}

