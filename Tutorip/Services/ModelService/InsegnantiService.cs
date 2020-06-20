using System;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services
{
    class InsegnantiService
    {
        public static async Task<Insegnante[]> GetInsegnanti(Filtri filtri)
        {
            return await InsegnantiRepository.GetInsegnanti(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
        }

        public static async void Save(Insegnante i)
        {
            Console.WriteLine("prova2");
            await InsegnantiRepository.SaveAsync(i, Constants.TutoripEndPoint + "/insegnante/create.php/");
        }
    }
}
