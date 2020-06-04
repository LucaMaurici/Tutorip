using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tutorip.Droid;
using Xamarin.Forms;

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