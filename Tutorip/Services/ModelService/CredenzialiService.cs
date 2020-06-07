using System;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services
{
    public static class CredenzialiService
    {
        public static async void Salva(Credenziali c)
        {
            bool esito = (bool) await CredenzialiRepository.Esiste(c, Constants.TutoripEndPoint + "/credenziali/checkEmail.php/");
            if (esito)
            {
                CredenzialiRepository.Save(c, Constants.TutoripEndPoint + "/credenziali/create.php/");
                Console.WriteLine("SUCCESSO!");
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
