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
    }
}
