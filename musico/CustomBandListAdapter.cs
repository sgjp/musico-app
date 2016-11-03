using System;
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Media;

namespace musico
{
	public class CustomBandListAdapter: BaseAdapter<string> {
		
		string[] names ;
		float[] prices ;
		float[] ratings ;

		Activity context;

		public CustomBandListAdapter(Activity context, string[] names, float[] prices, float[] ratings) : base() {
			this.context = context;
			this.names = names;
			this.prices = prices;
			this.ratings = ratings;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position] {  
			get { return names[position]; }
		}

		public override int Count {
			get { return names.Length; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.RowBandList, null);
			view.FindViewById<TextView>(Resource.Id.Name).Text = names[position];
			view.FindViewById<TextView>(Resource.Id.Price).Text = "$"+prices[position];
			view.FindViewById<RatingBar>(Resource.Id.Rating).Rating = ratings[position];

			return view;
		}

	

	}
}