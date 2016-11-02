
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

		protected override void OnCreate (Bundle savedInstanceState)
		{
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

			searchBTN.Click += SearchBTN_Click;
		
		}

		void SearchBTN_Click (object sender, EventArgs e)
		{
			string na
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

