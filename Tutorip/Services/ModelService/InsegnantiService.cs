using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;
using Tutorip.Services.ModelService;
using Xamarin.Essentials;

namespace Tutorip.Services
{
    class InsegnantiService
    {
        public static async Task<RisultatoRicercaInsegnanti[]> GetInsegnanti(Filtri filtri)
        {
            RisultatoRicercaInsegnanti[] risultati = await InsegnantiRepository.GetInsegnanti(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
            if (risultati != null)
            {
                foreach (RisultatoRicercaInsegnanti r in risultati)
                {
                    if (r.valutazioneMedia == "0")
                    {
                        r.valutazioneMedia = "-";
                    }
                    else
                    {
                        r.valutazioneMedia = ArrotondaStringa(r.valutazioneMedia);
                    }
                }
            }
            return risultati;
        }

        public static void Save(Insegnante i)
        {
            InsegnantiRepository.SaveAsync(i, Constants.TutoripEndPoint + "/insegnante/create.php/");
        }

        internal static async Task<Insegnante> getInsegnante(int id)
        {
            Insegnante i = await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
            
            if (i.valutazioneMedia == "0")
            {
                i.valutazioneMedia = "-";
            }
            else
            {
                i.valutazioneMedia = ArrotondaStringa(i.valutazioneMedia);
            }
            return i;
        }

        public static void aggiungiPreferito(int cod_utente, int cod_insegnante)
        {
            InsegnantiRepository.aggiungiPreferito(cod_utente, cod_insegnante, Constants.TutoripEndPoint + "/preferiti/savePreferiti.php/");
        }

        internal static async Task<RisultatoRicercaInsegnanti[]> getPreferiti(int idUtente) 
        {
            RisultatoRicercaInsegnanti[] risultati = await InsegnantiRepository.getPreferiti(idUtente, Constants.TutoripEndPoint + "/preferiti/findPreferitiById.php/");
            if (risultati != null)
            {
                foreach (RisultatoRicercaInsegnanti r in risultati)
                {
                    if (r.valutazioneMedia == "0")
                    {
                        r.valutazioneMedia = "-";
                    }
                    else
                    {
                        r.valutazioneMedia = ArrotondaStringa(r.valutazioneMedia);
                    }
                }
            }
            
            return risultati;
        }

        private static string ArrotondaStringa(string num)
        {
            if (num.Length <= 3)
                return num;
            if (num != "0")
            {
                var index = num.IndexOf(".");
                var n = float.Parse(num.Substring(index + 1, 1));
                if (n >= 5)
                {
                    num = num.Substring(0, index + 1) 
                        + (float.Parse(num.Substring(index+1, 1)) + 1).ToString();
                }
                else
                    num.Substring(0, index + 2);
            }
            return num;
        }

    }
}
