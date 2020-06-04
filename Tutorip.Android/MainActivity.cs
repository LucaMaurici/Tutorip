﻿using System;

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

namespace Tutorip.Droid
{
    [Activity(Label = "Tutorip", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        public static GoogleAuthenticator Auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);
            var googleLoginButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            googleLoginButton.Click += OnGoogleLoginButtonClicked;

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            StartActivity(intent);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            // Retrieve the user's email address
            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);

            // Display it on the UI
            var googleButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            googleButton.Text = $"Connected with {email}";
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