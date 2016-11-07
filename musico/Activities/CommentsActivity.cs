
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
	public class CommentsActivity : Activity
	{
		private ListView commentsLV;

		private TextView bandNameTV;

		private TextView genreTV;

		private TextView avgPriceTV;

		private TextView commentsTotalTV;

		private Button addCommentBtn;

		private string userId;

		private int commentsTotal;

		private Band band;

		private string bandName;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			userId = this.Intent.GetStringExtra ("id");

			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.Comments);
			base.OnCreate (savedInstanceState);

			initBandInfo ();

			initComments ();

			// Create your application here
		}

		private void initBandInfo(){

			bandName = this.Intent.GetStringExtra ("name");

			band = MusicoConnUtil.GetBandByNameAsync (bandName);

			bandNameTV = FindViewById<TextView> (Resource.Id.BandName);

			avgPriceTV = FindViewById<TextView> (Resource.Id.AvgPrice);

			genreTV = FindViewById<TextView> (Resource.Id.Genre);

			addCommentBtn = FindViewById<Button> (Resource.Id.AddComment);

			addCommentBtn.Click += AddCommentBtn_Click;

			bandNameTV.Text = band.Name;
			avgPriceTV.Text = "$" + band.AvgPrice;
			genreTV.Text = band.Genre;

		}

		void AddCommentBtn_Click (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof (AddCommentActivity));
			intent.PutExtra ("name", band.Name);
			intent.PutExtra ("id", band.Id);
			intent.PutExtra ("userId", userId);

			StartActivity (intent);
		}

		private void initComments(){
			commentsLV = FindViewById<ListView> (Resource.Id.Comments);
			commentsTotalTV = FindViewById<TextView> (Resource.Id.CommentsTotal);

			string[] comments = new string[band.Comments.Count];
			int[] types = new int[band.Comments.Count];

			for (int i=0; i < band.Comments.Count; i++){
				comments [i] = band.Comments [i].CommentText;
				types [i] = band.Comments [i].Type;
				commentsTotal = commentsTotal + band.Comments [i].Type;

			}


			CustomCommentListAdapter listAdapter = new CustomCommentListAdapter (this, comments, types);

			if (commentsTotal>0){
				commentsTotalTV.Text = "+"+commentsTotal;
			}else{
				commentsTotalTV.Text =  commentsTotal+"";
			}
			commentsLV.SetAdapter (listAdapter);



		}
	}
}

