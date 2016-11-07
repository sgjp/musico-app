using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;
using Musico;
using Android.Content;

namespace musico
{
	[Activity (Label = "Musico", MainLauncher = true, Icon = "@mipmap/logosmall")]	
	public class LoginActivity : Activity
	{
		private Button btnLogin;
		private Button btnCancel;
		private Button btnSignup;

		private EditText login_accounts;
		private EditText login_password;


		private LinearLayout ll_loginpage;

		private string email;
		private string password;


		private int result;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Login);

			btnLogin = FindViewById<Button> (Resource.Id.btn_login);
			btnCancel = FindViewById<Button> (Resource.Id.btn_cancel);
			btnSignup = FindViewById<Button> (Resource.Id.btn_signup);
			login_accounts = FindViewById<EditText> (Resource.Id.login_accounts);
			login_password = FindViewById<EditText> (Resource.Id.login_password);

			ll_loginpage = FindViewById<LinearLayout> (Resource.Id.loginpage);
				
			btnLogin.Click += BtnLogin_Click;
			btnCancel.Click += BtnCancel_Click;
			btnSignup.Click += BtnSignup_Click;

			//TODO del:
			login_accounts.Text = "Juan";
			login_password.Text ="test";
		}

		void BtnCancel_Click (object sender, EventArgs e)
		{
			
		}

		public async void BtnLogin_Click (object sender, EventArgs e)
		{
			email = login_accounts.Text;
			password = login_password.Text;
			bool isNetWorking = Utils.isNetworkAvailable (this);


			if (isNetWorking == true) {
				if (string.IsNullOrEmpty (email) || string.IsNullOrEmpty (password)) {
					DialogFactory.ToastDialog (this, "Login", "Username and password cannot be empty!", 0);


				}else {
					//check email
					Match match = Regex.Match (email, "^(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w{2,3}){1,3})$");
					//if (match.Success) {

					ll_loginpage.Alpha = 0.5f;

					try {
							result = await MusicoConnUtil.AuthenticateUser(email, password);
						} catch (Exception) {
							ll_loginpage.Alpha = 1.0f;
							AlertDialog.Builder ab = new AlertDialog.Builder (this);
							ab.SetTitle ("Server busy");
							ab.SetMessage ("Server is busy,please try again later!");
							ab.SetPositiveButton ("confirm", delegate(object sndr, DialogClickEventArgs ee) {
							});
							ab.Create ().Show ();
						}

							if (result >0 ) {
								//login successfull

								Toast.MakeText (this, "Login successfull!", ToastLength.Short).Show ();
								Intent intent = new Intent (this, typeof(HomeActivity));
								intent.PutExtra ("email", email);
								intent.PutExtra ("id", result+"");
								StartActivity (intent);
								Finish ();
							} else {
								ll_loginpage.Alpha = 1.0f;
								//failed
								DialogFactory.ToastDialog (this, "Login", "Username or password is not correct!", 0);//to do:specific error msg
							}

					//} else {
					//	ll_loginpage.Alpha = 1.0f;
					//	DialogFactory.ToastDialog (this, "Login", "Email format is incorrent!", 0);
					//}
				}
			} else {
				DialogFactory.ToastDialog (this, "Connect Error", "There is no internet,please connect the internet!", 0);
			}

		}

		void BtnSignup_Click (object sender, EventArgs e)
		{

		}


	}
}

