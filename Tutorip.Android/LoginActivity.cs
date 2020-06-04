using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
//using Tutorip.GoogleAuthentication.Authentication;
using Tutorip.GoogleAuthentication.Services;
//using Android.Content.Res;
using Tutorip.Services.GoogleServices;
using Android.Content;

namespace Tutorip.Droid
{
    [Activity(Label = "Login", MainLauncher = false, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LoginActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        public static GoogleAuthenticator Auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("Loginactivity.OnCreate1");
            base.OnCreate(savedInstanceState);
            Console.WriteLine("Loginactivity.OnCreate2");
            SetContentView(Resource.Layout.Login);
            Console.WriteLine("Loginactivity.OnCreate3");
            Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);
            Button googleLoginButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            googleLoginButton.Click += OnGoogleLoginButtonClicked;
            Console.WriteLine("Loginactivity.OnCreate4");
        }

        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            Console.WriteLine("Loginactivity.OnGoogleLoginButtonClicked1");
            var authenticator = Auth.GetAuthenticator();
            Console.WriteLine("Loginactivity.OnGoogleLoginButtonClicked2");
            var intent = authenticator.GetUI(this);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            Console.WriteLine("Loginactivity.OnGoogleLoginButtonClicked3");
            StartActivity(intent);
            Console.WriteLine("Loginactivity.OnGoogleLoginButtonClicked4");
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            Console.WriteLine("OnAuthenticationCompleted1");
            // Retrieve the user's email address
            var googleService = new GoogleService();
            Console.WriteLine("OnAuthenticationCompleted2");
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);
            Console.WriteLine(email);
            Console.WriteLine("OnAuthenticationCompleted3");

            // Display it on the UI
            var googleButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            googleButton.Text = $"Connected with {email}";
            //SetContentView(Resource.Layout.Login);
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