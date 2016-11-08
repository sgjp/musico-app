

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
using Musico;

namespace musico
{
	[Activity (Label = "Musico")]			
	public class BookActivity : Activity
	{
		public Button bookBtn;

		public TextView descriptionTV;

		public TextView dateTV;

		public TextView bandNameTV;

		public TextView avgPriceTV ;

		public TextView genreTV;

		public TextView locationTV;

		public RatingBar ratingRB;

		private string userId;
		private string bandId;
		private string bandName;

		public string description;

		public string dateString;

		public DateTime date;

		public Band band;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			
			userId = this.Intent.GetStringExtra ("userId");
			bandId = this.Intent.GetStringExtra ("id");
			bandName = this.Intent.GetStringExtra ("name");

			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.Book);

			base.OnCreate (savedInstanceState);

			initBandInfo ();

			bookBtn = FindViewById<Button> (Resource.Id.Book);

			descriptionTV = FindViewById<TextView> (Resource.Id.Description);

			dateTV = FindViewById<TextView> (Resource.Id.DateSearch);

			bookBtn.Click += BookBtn_Click;
		}

		async void BookBtn_Click (object sender, EventArgs e)
		{
			description = descriptionTV.Text;

			dateString = dateTV.Text;



			foreach (Booking booking in band.Bookings){
				
				if(booking.Date.ToString ("yyyy-MM-dd") == dateString){
					Toast.MakeText (this, "Sorry, this artist is not available in the date you selected", ToastLength.Long).Show ();
					return;
				}
			}
			int result = await MusicoConnUtil.AddBooking (description, Convert.ToDateTime (dateString), userId, bandId);

			if (result<0){
				Toast.MakeText (this, "An error has ocurred, please try again", ToastLength.Short).Show ();
				return;
			}

			Toast.MakeText (this, "You have booked this artist!", ToastLength.Long).Show ();
			Intent intent = new Intent (this, typeof(HomeActivity));
		
			intent.PutExtra ("id", userId);
			StartActivity (intent);
			Finish ();

		}

		private void initBandInfo(){



			band = MusicoConnUtil.GetBandByNameAsync (bandName);

			bandNameTV = FindViewById<TextView> (Resource.Id.BandName);

			avgPriceTV = FindViewById<TextView> (Resource.Id.AvgPrice);

			genreTV = FindViewById<TextView> (Resource.Id.Genre);

			locationTV = FindViewById<TextView> (Resource.Id.Location);

			ratingRB = FindViewById<RatingBar> (Resource.Id.Rating);

			bandNameTV.Text = band.Name;
			avgPriceTV.Text = "$" + band.AvgPrice;
			genreTV.Text = band.Genre;
			locationTV.Text = band.Location;
			ratingRB.Rating = band.AvgRate;



		}
	}
}

