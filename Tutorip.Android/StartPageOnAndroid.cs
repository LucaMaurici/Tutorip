﻿using Android.Content;
using Tutorip.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(StartPageOnAndroid))]
namespace Tutorip.Droid
{
    class StartPageOnAndroid : INativePages
    {
        public void StartPage()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(LoginActivity) );
            intent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}