using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System;

namespace Tutorip.Droid
{
    [Activity(Label = "GoogleAuthInterceptor", LaunchMode = LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[]
            {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
            },
            DataSchemes = new[]
            {
                // First part of the redirect url (Package name)
                "com.mvm.tutorip"
            },
            DataPaths = new[]
            {
                // Second part of the redirect url (Path)
                "/oauth2redirect"
            }
        )
    ]
    public class GoogleAuthInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("GoogleAuthInterceptor.OnCreate1");
            base.OnCreate(savedInstanceState);
            Console.WriteLine("GoogleAuthInterceptor.OnCreate2");
            Android.Net.Uri uri_android = Intent.Data;

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(uri_android.ToString());
            Console.WriteLine("GoogleAuthInterceptor.OnCreate3");
            Console.WriteLine(uri_netfx);
            // Send the URI to the Authenticator for continuation
            LoginActivity.Auth?.OnPageLoading(uri_netfx);
            Console.WriteLine("GoogleAuthInterceptor.OnCreate4");
            Finish();
        }
    }
}