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
            return await InsegnantiRepository.GetInsegnanti(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
        }

        public static void Save(Insegnante i)
        {
            InsegnantiRepository.SaveAsync(i, Constants.TutoripEndPoint + "/insegnante/create.php/");
        }

        /*internal static async Task<Insegnante> getInsegnante(int id)
        {
            return await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
        }*/

        internal static async Task<Insegnante> getInsegnante(int id)
        {
            //Materia[] materie = await MaterieService.getMaterieInsegnante(id);
            //Recensione[] recensioni = await RecensioniService.GetRecensioniInsegnante(id);
            //SezioneProfilo[] descrizione = await DescrizioneService.getDescrizione(id);
            Insegnante i = await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
            //i.materie = new List<Materia>();
            //i.recensioni = new List<Recensione>();
            //i.descrizione = new List<SezioneProfilo>();
            /*
            if (materie != null)
            {
                foreach (Materia m in materie)
                {
                    i.materie.Add(m);
                }
            }
            if (recensioni != null)
            {
                foreach (Recensione r in recensioni)
                {
                    i.recensioni.Add(r);
                }
            }
            if (descrizione != null)
            {
                foreach (SezioneProfilo sezione in descrizione)
                {
                    i.descrizione.Add(sezione);
                }
            }*/
            if (i.valutazioneMedia == null || i.valutazioneMedia == "")
            {
                //qualcosa per togliere la valutazione
                i.valutazioneMedia = "-";
            }
            return i;
        }

        public static void aggiungiPreferito(int cod_utente, int cod_insegnante)
        {
            InsegnantiRepository.aggiungiPreferito(cod_utente, cod_insegnante, Constants.TutoripEndPoint + "/preferiti/savePreferiti.php/");
        }

        internal static async Task<RisultatoRicercaInsegnanti[]> getPreferiti(int idUtente) 
        {
            return await InsegnantiRepository.getPreferiti(idUtente, Constants.TutoripEndPoint + "/preferiti/findPreferitiById.php/");
        }
    }
}
