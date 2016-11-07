
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
	[Activity (Label = "AddReviewActivity")]			
	public class AddReviewActivity : Activity
	{
		private Button addReviewBtn;

		private TextView commentTV;

		private RatingBar overallRB;

		private RatingBar qualityRB;

		private RatingBar punctualityRB;

		private RatingBar flexibilityRB;

		private RatingBar enthusiasmRB;

		private RatingBar similarityRB;

		private string comment;

		private float overall;

		private float quality;

		private float punctuality;

		private float flexibility;

		private float enthusiasm;

		private float similarity;



		private string userId;
		private string bandId;
		private string bandName;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			userId = this.Intent.GetStringExtra ("userId");
			bandId = this.Intent.GetStringExtra ("id");
			bandName = this.Intent.GetStringExtra ("name");

			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.AddReview);

			base.OnCreate (savedInstanceState);

			FindViewById<TextView> (Resource.Id.BandName).Text = bandName;
			initComponents ();

		}

		private void initComponents(){
			FindViewById<TextView> (Resource.Id.BandName).Text = bandName;
			commentTV = FindViewById<TextView> (Resource.Id.Comment);
			addReviewBtn = FindViewById<Button> (Resource.Id.AddReview);
			addReviewBtn.Click += AddReviewBtn_Click;
			overallRB = FindViewById<RatingBar> (Resource.Id.Overall);
			qualityRB = FindViewById<RatingBar> (Resource.Id.Quality);
			punctualityRB = FindViewById<RatingBar> (Resource.Id.Punctuality);
			flexibilityRB = FindViewById<RatingBar> (Resource.Id.Flexibility);
			enthusiasmRB = FindViewById<RatingBar> (Resource.Id.Enthusiasm);
			similarityRB = FindViewById<RatingBar> (Resource.Id.Similarity);

		}

		public async void AddReviewBtn_Click (object sender, EventArgs e)
		{
			comment = commentTV.Text;
			overall = overallRB.Rating;
			quality = qualityRB.Rating;
			punctuality = punctualityRB.Rating;
			flexibility = flexibilityRB.Rating;
			enthusiasm = enthusiasmRB.Rating;
			similarity = similarityRB.Rating;

			int result = await MusicoConnUtil.AddReview (comment, overall, quality, punctuality, flexibility, enthusiasm, similarity, userId, bandId);

			Intent intent = new Intent (this, typeof (ReviewsActivity));
			intent.PutExtra ("name", bandName);
			intent.PutExtra ("id", userId);

			Toast.MakeText (this, "Review added!", ToastLength.Short).Show ();
			StartActivity (intent);
			Finish ();

		}
	}
}

