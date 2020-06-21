using System;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Tutorip.GoogleAuthentication.Services;
using Tutorip.Services.GoogleServices;
using Android.Content;
using Xamarin.Auth;
using Xamarin.Essentials;
using Tutorip.Models;
using Tutorip.Services;

namespace Tutorip.Droid
{
    [Activity(Label = "Login", MainLauncher = false, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class GoogleLoginActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        public static GoogleAuthenticator Auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.Login);
            Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);
            CustomTabsConfiguration.CustomTabsClosingMessage = null;
            Button googleLoginButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            //googleLoginButton.Click += OnGoogleLoginButtonClicked;
            this.OnGoogleLoginButtonClicked();
            //googleLoginButton.Text = "Connesso con " + Preferences.Get("nome", "Login with Google");
        }

        private void OnGoogleLoginButtonClicked() //c'erano sender e eventargs
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            var googleService = new GoogleService();
            var profile = await googleService.GetProfileAsync(token.TokenType, token.AccessToken);
            var googleButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            //googleButton.Text = $"Connesso con {profile.given_name}";
            Preferences.Set("tokenType", token.TokenType);
            Preferences.Set("accessToken", token.AccessToken);
            Preferences.Set("email", profile.email);
            Preferences.Set("nome", profile.given_name);
            Preferences.Set("cognome", profile.family_name);
            var credenziali = new Credenziali(profile.email, token);
            UtenteCredenzialiService.Salva(credenziali, new Utente(profile.given_name, profile.family_name));
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