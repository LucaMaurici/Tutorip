using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Widget;
using Java.Lang;
using Java.Security;
using System;
using System.Collections.Generic;
using System.Json;
using Tutorip.Services.FacebookAuthentication;
using Tutorip.Services.FacebookServices;
using Xamarin.Auth;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace Tutorip.Droid
{
    [Activity(Label = "com.mvm.tutorip.FacebookLoginActivity", MainLauncher = false, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class FacebookLoginActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IFacebookCallback {
        TextView userNameText, emailText;
        LoginButton facebookLoginButton;
        ICallbackManager callbackManager;
        FacebookProfileTracker profileTracker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FacebookLogin);

            facebookLoginButton = (LoginButton)FindViewById(Resource.Id.loginButton);
            userNameText = (TextView)FindViewById(Resource.Id.usernameText);
            emailText = (TextView)FindViewById(Resource.Id.emailText);
            facebookLoginButton.SetReadPermissions(new List<string> { "public_profile", "email" });
            
            callbackManager = CallbackManagerFactory.Create();
            facebookLoginButton.RegisterCallback(callbackManager, this);
            profileTracker = new FacebookProfileTracker();
    }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        public void OnCancel() {
            //Questo metodo viene richiamato solo se l'utente chiude la finestra di login senza concludere la procedura. Un toast è più che sufficiente per informare l'utente che 
            //L'operazione è stata cancellata
            Console.WriteLine("llalalala");
            Toast.MakeText(this, "Cancellato", ToastLength.Long).Show();
        }

        public void OnError(FacebookException error) {
            Console.WriteLine(error.ToString());
            //Qui si deve implementare la logica in caso di errori durante il login
        }

        public void OnSuccess(Java.Lang.Object result) {
            //Qui si deve implementare la logica per prendere i dati dal profilo dell'utente e poterli utilizzare nell'applicazione.
            Console.WriteLine("sususususus");
            Console.WriteLine(result);
            Toast.MakeText(this, "Success", ToastLength.Long).Show();
        }
    }
}