
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
	public class AddCommentActivity : Activity
	{
		private string userId;
		private string bandId;
		private string bandName;

		private string comment;

		private string type;

		private TextView commentTV;

		private Spinner qualificationSPN;

		private Button addCommentBtn;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			userId = this.Intent.GetStringExtra ("userId");
			bandId = this.Intent.GetStringExtra ("id");
			bandName = this.Intent.GetStringExtra ("name");

			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.AddComment);

			base.OnCreate (savedInstanceState);

			FindViewById<TextView> (Resource.Id.BandName).Text = bandName;
			initComponents ();
		}

		private void initComponents(){

			commentTV = FindViewById<TextView> (Resource.Id.Comment);
			qualificationSPN = FindViewById<Spinner> (Resource.Id.Qualification);

			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.qualifications_array, Resource.Layout.SpinnerItem);
			
			qualificationSPN.Adapter = adapter;

			addCommentBtn = FindViewById<Button> (Resource.Id.AddComment);

			addCommentBtn.Click += AddCommentBtn_Click;

		}

		private async void AddCommentBtn_Click (object sender, EventArgs e)
		{
			type = qualificationSPN.SelectedItem.ToString();
			comment = commentTV.Text;

			int result = await MusicoConnUtil.AddComment(comment, type, bandId, userId);

			Intent intent = new Intent (this, typeof (CommentsActivity));
			intent.PutExtra ("name", bandName);
			intent.PutExtra ("id", userId);

			Toast.MakeText (this, "Comment added!", ToastLength.Short).Show ();
			StartActivity (intent);
			Finish ();
		}


	}
}

