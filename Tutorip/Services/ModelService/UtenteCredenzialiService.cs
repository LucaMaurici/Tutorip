using Android.Preferences;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
            bool esito = (bool)await CredenzialiRepository.Esiste(c, Constants.TutoripEndPoint + "/credenziali/checkEmail.php/");
            if (esito)
            {
                CredenzialiRepository.Save(c, Constants.TutoripEndPoint + "/credenziali/create.php/");
                Console.WriteLine(Preferences.Get("email", null));
                int id = 1;
                id = await CredenzialiRepository.getId(Preferences.Get("email", null), Constants.TutoripEndPoint + "/credenziali/getId.php/");
                Console.WriteLine("ID!!!!!!!!!!!: " + id);
                if (id != 0)
                {
                    Preferences.Set("id", id.ToString());
                    u.id = id;
                    UtentiRepository.SaveAsync(u, Constants.TutoripEndPoint + "/utente/create.php/");
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
