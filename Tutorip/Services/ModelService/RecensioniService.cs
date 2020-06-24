using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services.ModelService
{
    class RecensioniService
    {
        public static void Save(Recensione recensione)
        {
            RecensioniRepository.Save(recensione, Constants.TutoripEndPoint + "/recensione/create.php/");
        }

        public static async Task<Recensione[]> GetRecensioniInsegnante(int idInsegnante)
        {
            return await RecensioniRepository.GetRecensioniInsegnante(idInsegnante, Constants.TutoripEndPoint + "/recensione/findRecensioniByIdInsegnante.php/");
        }
            

    }
}
