using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Repository;

namespace Tutorip.Services.ModelService
{
    class MaterieService
    {
        public static async Task<List<string>> getMaterie()
        {
            return await MaterieRepository.getAllMaterie(Constants.TutoripEndPoint + "/materie/getAllMaterie.php/");
        }
    }
}
