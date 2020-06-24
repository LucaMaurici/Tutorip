using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;
using Tutorip.Services.ModelService;

namespace Tutorip.Services
{
    class InsegnantiService
    {
        public static async Task<RisultatoRicercaInsegnanti[]> GetInsegnanti(Filtri filtri)
        {
            Console.WriteLine("PROVA3");
            return await InsegnantiRepository.GetInsegnanti(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
        }

        public static async void Save(Insegnante i)
        {
            await InsegnantiRepository.SaveAsync(i, Constants.TutoripEndPoint + "/insegnante/create.php/");
        }

        /*internal static async Task<Insegnante> getInsegnante(int id)
        {
            return await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
        }*/

        internal static async Task<Insegnante> getInsegnante(int id)
        {
            Materia[] materie = await MaterieService.getMaterieInsegnante(id);
            Recensione[] recensioni = await RecensioniService.GetRecensioniInsegnante(id);
            Insegnante i = await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
            i.materie = new List<Materia>();
            foreach (Materia m in materie)
            {
                i.materie.Add(m);
            }
            i.recensioni = recensioni;
            return i;
        }

        public static void aggiungiPreferito(int cod_utente, int cod_insegnante)
        {
            InsegnantiRepository.aggiungiPreferito(cod_utente, cod_insegnante, Constants.TutoripEndPoint + "/insegnante/aggiungiPreferito.php/");
        }

        internal static async Task<RisultatoRicercaInsegnanti[]> getPreferiti(int idUtente) 
        {
            return await InsegnantiRepository.getPreferiti(idUtente, Constants.TutoripEndPoint + "/insegnante/getPreferiti/");
        }
    }
}
