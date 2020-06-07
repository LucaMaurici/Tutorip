using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;
using tutoripProva.Models;

namespace Tutorip.Services
{
    class InsegnantiService
    {
        public static async Task<Insegnante[]> GetInsegnanti(Filtri filtri)
        {
            ElencoInsegnanti elenco = await InsegnantiRepository.GetInsegnanti(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
            return elenco.Insegnanti;
        }
        
    }
}
