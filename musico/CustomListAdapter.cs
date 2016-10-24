using System;
using Android.Widget;
using Android.App;
using Android.Views;

namespace musico
{
	public class CustomListAdapter: BaseAdapter<string> {
		
		string[] items;
		Activity context;

		public CustomListAdapter(Activity context, string[] items) : base() {
			this.context = context;
			this.items = items;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position] {  
			get { return items[position]; }
		}

		public override int Count {
			get { return items.Length; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.Row, null);
			view.FindViewById<TextView>(Resource.Id.text1).Text = items[position];

			return view;
		}


	}
}