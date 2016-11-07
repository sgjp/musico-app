
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
	public class ReviewsActivity : Activity
	{
		private ListView reviewsLV;

		private TextView bandNameTV;

		private TextView genreTV;

		private TextView avgPriceTV;

		private Button addReviewBtn;

		private string userId;

		private Band band;

		private string bandName;

		string[] comments;

		string[] ratingsQuality;
		string[] ratingsPunctuality;
		string[] ratingsFlexibility;
		string[] ratingsEnthusiasm;
		string[] ratingsSimilarity;

		float[] ratingsOverall;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			userId = this.Intent.GetStringExtra ("id");
			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.Reviews);
			base.OnCreate (savedInstanceState);

			initBandInfo ();

			initReviews ();

			addReviewBtn = FindViewById<Button> (Resource.Id.AddReview);

			addReviewBtn.Click += AddReviewBtn;

			// Create your application here
		}

		void AddReviewBtn (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof (AddReviewActivity));
			intent.PutExtra ("name", band.Name);
			intent.PutExtra ("id", band.Id);
			intent.PutExtra ("userId", userId);

			StartActivity (intent);
		}

		private void initBandInfo(){

			bandName = this.Intent.GetStringExtra ("name");

			band = MusicoConnUtil.GetBandByNameAsync (bandName);

			bandNameTV = FindViewById<TextView> (Resource.Id.BandName);

			avgPriceTV = FindViewById<TextView> (Resource.Id.AvgPrice);

			genreTV = FindViewById<TextView> (Resource.Id.Genre);

			bandNameTV.Text = band.Name;
			avgPriceTV.Text = "$" + band.AvgPrice;
			genreTV.Text = band.Genre;

		}

		private void initReviews(){
			reviewsLV = FindViewById<ListView> (Resource.Id.reviews);

			comments = new string[band.Reviews.Count];

			ratingsQuality = new string[band.Reviews.Count];
			ratingsPunctuality = new string[band.Reviews.Count];
			ratingsFlexibility = new string[band.Reviews.Count];
			ratingsEnthusiasm = new string[band.Reviews.Count];
			ratingsSimilarity = new string[band.Reviews.Count];

			ratingsOverall = new float[band.Reviews.Count];

			for (int i=0; i < band.Reviews.Count; i++){
				comments [i] = band.Reviews [i].Comment;

				ratingsQuality [i] = band.Reviews [i].RateQuality;
				ratingsPunctuality [i] = band.Reviews [i].RatePunctuality;
				ratingsFlexibility [i] = band.Reviews [i].RateFlexibility;
				ratingsEnthusiasm [i] = band.Reviews [i].RateEnthusiasm;
				ratingsSimilarity [i] = band.Reviews [i].RateSimilarity;

				ratingsOverall [i] = float.Parse(band.Reviews [i].Rate);


			}


			CustomReviewListAdapter listAdapter = new CustomReviewListAdapter (this, comments, ratingsOverall);

			reviewsLV.SetAdapter (listAdapter);

			reviewsLV.ItemClick += ReviewsLV_ItemClick;

			
		}

		void ReviewsLV_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			

			Bundle data = new Bundle();
			data.PutString("rateQuality", ratingsQuality[e.Position]);
			data.PutString("ratePunctuality", ratingsPunctuality[e.Position]);
			data.PutString("rateFlexibility", ratingsFlexibility[e.Position]);
			data.PutString("rateEnthusiasm", ratingsEnthusiasm[e.Position]);
			data.PutString("rateSimilarity", ratingsSimilarity[e.Position]);

			FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			DialogReviewDetail reviewDetail = new DialogReviewDetail (data);

			reviewDetail.Arguments = data;
			reviewDetail.Show(transaction, "dialog fragment");

		}
	}
}

