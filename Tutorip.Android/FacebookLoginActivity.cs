using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Tutorip.Services.FacebookAuthentication;
using Tutorip.Services.FacebookServices;
using Xamarin.Essentials;

namespace Tutorip.Droid
{
    [Activity(Label = "Xamarin_FacebookAuth.Android", MainLauncher = false, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class FacebookLoginActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IFacebookAuthenticationDelegate
    {
        private static FacebookAuthenticator _auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FacebookLogin);
            _auth = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, this);
            var facebookLoginButton = FindViewById<Button>(Resource.Id.facebookLoginButton);
            facebookLoginButton.Click += OnFacebookLoginButtonClicked;
            facebookLoginButton.Text = "Connesso con " + Preferences.Get("email", "Login with Facebook");
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            var authenticator = _auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            //intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop); 
            StartActivity(intent);
        }

        public async void OnAuthenticationCompleted(FacebookOAuthToken token)
        {
            // Retrieve the user's email address
            var facebookService = new FacebookService();
            var email = await facebookService.GetEmailAsync(token.AccessToken);

            // Display it on the UI
            var facebookButton = FindViewById<Button>(Resource.Id.facebookLoginButton);
            facebookButton.Text = $"Connesso con {email}";
        }

        public void OnAuthenticationCanceled()
        {
            new AlertDialog.Builder(this)
                .SetTitle("Authentication canceled")
                .SetMessage("You didn't completed the authentication process")
                .Show();
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            new AlertDialog.Builder(this)
                .SetTitle(message)
                .SetMessage(exception?.ToString())
                .Show();
        }
    }
}