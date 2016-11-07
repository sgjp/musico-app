
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace musico
{
	public class DialogReviewDetail : DialogFragment
	{
		Bundle data;

		TextView qualityTV;

		TextView punctualityTV;

		TextView similarityTV;

		TextView enthusiasmTV;

		TextView flexibilityTV;

		public DialogReviewDetail (Bundle data) 
		{
			this.data = data;
		}

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.ReviewDetail, container, false);

			Dialog.SetCanceledOnTouchOutside (true);

			qualityTV = view.FindViewById<TextView> (Resource.Id.Quality);
			punctualityTV = view.FindViewById<TextView> (Resource.Id.Punctuality);
			similarityTV = view.FindViewById<TextView> (Resource.Id.Similarity);
			enthusiasmTV = view.FindViewById<TextView> (Resource.Id.Enthusiasm);
			flexibilityTV = view.FindViewById<TextView> (Resource.Id.Flexibility);

			qualityTV.Text = data.GetString ("rateQuality")+"/5";
			punctualityTV.Text = data.GetString ("ratePunctuality")+"/5";
			similarityTV.Text = data.GetString ("rateSimilarity")+"/5";
			enthusiasmTV.Text = data.GetString ("rateEnthusiasm")+"/5";
			flexibilityTV.Text = data.GetString ("rateFlexibility")+"/5";



			return view;
		}



		public override void OnActivityCreated (Android.OS.Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;

		}


	}
}

