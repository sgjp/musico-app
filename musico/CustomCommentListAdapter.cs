using System;
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Media;

namespace musico
{
	public class CustomCommentListAdapter: BaseAdapter<string> {
		
		string[] comments ;
		int[] types ;

		Activity context;

		public CustomCommentListAdapter(Activity context, string[] comments, int[] types ) : base() {
			this.context = context;

			this.comments = comments;

			this.types = types;
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
				view = context.LayoutInflater.Inflate(Resource.Layout.CommentRow, null);
			view.FindViewById<TextView>(Resource.Id.Comment).Text = comments[position];
			view.FindViewById<TextView> (Resource.Id.Type).Text = types[position]+"";




			return view;
		}

	

	}
}