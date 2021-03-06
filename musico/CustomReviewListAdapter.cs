﻿using System;
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Media;

namespace musico
{
	public class CustomReviewListAdapter: BaseAdapter<string> {
		
		string[] comments ;
		float[] ratingsOverall ;

		Activity context;

		public CustomReviewListAdapter(Activity context, string[] comments, float[] ratingsOverall ) : base() {
			this.context = context;

			this.comments = comments;

//			this.ratingsQuality = ratingsQuality;
//			this.ratingsPunctuality = ratingsPunctuality;
//			this.ratingsFlexibility = ratingsFlexibility;
//			this.ratingsEnthusiasm = ratingsEnthusiasm;
//			this.ratingsSimilarity = ratingsSimilarity;

			this.ratingsOverall = ratingsOverall;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position] {  
			get { return comments[position]; }
		}

		public override int Count {
			get { return comments.Length; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.ReviewRow, null);
			view.FindViewById<TextView>(Resource.Id.Comment).Text = comments[position];
			view.FindViewById<RatingBar>(Resource.Id.Rating).Rating = ratingsOverall[position];


			return view;
		}

	

	}
}