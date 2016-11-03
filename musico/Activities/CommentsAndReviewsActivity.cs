
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
	public class CommentsAndReviewsActivity : Activity
	{
		private ListView reviewsLV;
		private int id;

		private Band band;

		private string bandName;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.CommentsAndReviews);
			base.OnCreate (savedInstanceState);

			initBandInfo ();

			initReviews ();

			// Create your application here
		}

		private void initBandInfo(){

			bandName = this.Intent.GetStringExtra ("name");

			band = MusicoConnUtil.GetBandByNameAsync (bandName);

		}

		private void initReviews(){
			reviewsLV = FindViewById<ListView> (Resource.Id.reviews);

			string[] comments = new string[band.Reviews.Count];
			string[] ratingsQuality = new string[band.Reviews.Count];
			string[] ratingsPunctuality = new string[band.Reviews.Count];
			string[] ratingsFlexibility = new string[band.Reviews.Count];
			string[] ratingsEnthusiasm = new string[band.Reviews.Count];
			string[] ratingsSimilarity = new string[band.Reviews.Count];
			string[] ratingsOverall = new string[band.Reviews.Count];

			for (int i=0; i < band.Reviews.Count; i++){
				comments [i] = band.Reviews [i].Comment;

				ratingsQuality [i] = band.Reviews [i].RateQuality;
				ratingsPunctuality [i] = band.Reviews [i].RatePunctuality;
				ratingsFlexibility [i] = band.Reviews [i].RateFlexibility;
				ratingsEnthusiasm [i] = band.Reviews [i].RateEnthusiasm;
				ratingsSimilarity [i] = band.Reviews [i].RateSimilarity;
				ratingsOverall [i] = band.Reviews [i].Rate;

			}


			CustomReviewListAdapter listAdapter = new CustomReviewListAdapter (this, comments, ratingsQuality, ratingsPunctuality, ratingsFlexibility, ratingsEnthusiasm, ratingsSimilarity, ratingsOverall);

			reviewsLV.SetAdapter (listAdapter);

			
		}
	}
}

