
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
	public class DetailActivity : Activity
	{

		private string bandName;

		private Band band;

		private TextView bandNameTV;

		private TextView genreTV;

		private TextView avgPriceTV;

		private TextView locationTV;

		private TextView requerimentsTV;

		private TextView eventsTV;

		private Button commentsBTN;

		private Button bookBTN;

		private RatingBar ratingRB;

		private ImageButton facebookIBTN;

		private ImageButton youtubeIBTN;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.Detail);

			commentsBTN = FindViewById<Button> (Resource.Id.Comments);

			commentsBTN.Click += CommentsBTN_Click;


			initBandInfo ();
			initEvents ();
			initRating ();

		}

		void CommentsBTN_Click (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof (CommentsAndReviewsActivity));
			intent.PutExtra ("name", band.Name);

			StartActivity (intent);

		}

		private void initBandInfo(){
			
			bandName = this.Intent.GetStringExtra ("name");

			band = MusicoConnUtil.GetBandByNameAsync (bandName);

			bandNameTV = FindViewById<TextView> (Resource.Id.BandName);

			avgPriceTV = FindViewById<TextView> (Resource.Id.AvgPrice);

			genreTV = FindViewById<TextView> (Resource.Id.Genre);

			locationTV = FindViewById<TextView> (Resource.Id.Location);

			requerimentsTV = FindViewById<TextView> (Resource.Id.Requeriments);

			eventsTV = FindViewById<TextView> (Resource.Id.Events);

			ratingRB = FindViewById<RatingBar> (Resource.Id.Rating);


			facebookIBTN = FindViewById<ImageButton> (Resource.Id.FbImageBtn);

			youtubeIBTN = FindViewById<ImageButton> (Resource.Id.YoImageBtn);

			bandNameTV.Text = band.Name;
			avgPriceTV.Text = "$" + band.AvgPrice;
			genreTV.Text = band.Genre;
			locationTV.Text = band.Location;
			requerimentsTV.Text = band.Requirements;

			facebookIBTN.Click += FacebookIBTN_Click;
			youtubeIBTN.Click += YoutubeIBTN_Click;


		
		}

		void YoutubeIBTN_Click (object sender, EventArgs e)
		{
			var uri = Android.Net.Uri.Parse (band.Youtube);
			var intent = new Intent (Intent.ActionView, uri);
			StartActivity (intent);
		}

		void FacebookIBTN_Click (object sender, EventArgs e)
		{
			var uri = Android.Net.Uri.Parse (band.Facebook);
			var intent = new Intent (Intent.ActionView, uri);
			StartActivity (intent);
		}


		private void initRating(){
			ratingRB.Rating = band.AvgRate;
		}

		private void initEvents(){
			eventsTV.Text = "";
			int i = 0;
			foreach (Booking booking in band.Bookings){
				eventsTV.Text += booking.Date.ToString("MMMM dd, yyyy")+"\n";
				i++;
				if (i>=5){
					break;
				}
				
			}
		}
	}
}

