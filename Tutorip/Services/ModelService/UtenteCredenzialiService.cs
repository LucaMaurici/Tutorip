using Android.Preferences;
using System;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;
using Xamarin.Essentials;

namespace Tutorip.Services
{
    public static class UtenteCredenzialiService
    {
        public static async void Salva(Credenziali c, Utente u)
        {
            bool esito = (bool) await CredenzialiRepository.Esiste(c, Constants.TutoripEndPoint + "/credenziali/checkEmail.php/");
            if (esito)
            {
                CredenzialiRepository.Save(c, Constants.TutoripEndPoint + "/credenziali/create.php/");
                int id = await CredenzialiRepository.getIdAsync(Preferences.Get("email", null), Constants.TutoripEndPoint + "/credenziali/getId.php/");
                if (id != 0)
                {
                    Preferences.Set("id", id.ToString());
                    u.id = id;
                    UtentiRepository.SaveAsync(u, Constants.TutoripEndPoint + "/utenteVero/createVera.php/");
                    Console.WriteLine("SUCCESSO!");
                }
            }
            else
            {
                Console.WriteLine("FALLIMENTO!");
            }
        }

        public static async void Esiste(Credenziali c)
        {
            await CredenzialiRepository.Esiste(c, Constants.TutoripEndPoint + "/credenziali/checkEmail.php/");
        }
    }
}
