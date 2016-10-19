using System;
using Android.App;
using Android.Content;
using Android.Graphics;

namespace musico
{
	public static class DialogFactory
	{

		public static void ToastDialog (Context context, String title, String msg, int flag)
		{
			AlertDialog.Builder ab = new AlertDialog.Builder (context);
			ab.SetTitle (title);
			ab.SetMessage (msg);
			ab.SetPositiveButton ("confirm", delegate(object sender, DialogClickEventArgs e) {
				
			});
			ab.Create ().Show ();
		}

		public static void toastNegativePositiveDialog (Context context, String title, String msg, int flag)
		{
			AlertDialog.Builder ab = new AlertDialog.Builder (context);
			ab.SetTitle (title);
			ab.SetMessage (msg);
			ab.SetPositiveButton ("Confirm", delegate(object sender, DialogClickEventArgs e) {
				

			});
			ab.SetNegativeButton ("Cancel", delegate(object sender, DialogClickEventArgs e) {

			});
			ab.Create ().Show ();
		}

	}
}


