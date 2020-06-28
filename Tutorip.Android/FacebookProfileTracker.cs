using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;

namespace Tutorip.Droid
{
    class FacebookProfileTracker : ProfileTracker {

        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile) {
            try
            {
                //Qui va messa la logica per poter tracciare le modifiche che l'utente fa al profilo. In sostanza questo metodo viene richiamato ogni qualvolta l'utente modifica il suo profilo facebook.
                //La logica che andrebbe implementata è relativa all'aggiornamento dei dati persistiti nell'applicazione: Nome, email, foto profilo, ecc ecc.
            } catch(NullReferenceException e)
            {
                Android.Util.Log.Info("ProfileTracker", "L'utente che si stava precedentemente tracciando ha tolto il login con facebook");
            }
        }
    }
}