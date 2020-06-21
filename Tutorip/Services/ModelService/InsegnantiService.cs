using System;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

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

        internal static async Task<Insegnante> getInsegnante(int id)
        {
            return await InsegnantiRepository.findInsegnanteById(id, Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
        }
    }
}
