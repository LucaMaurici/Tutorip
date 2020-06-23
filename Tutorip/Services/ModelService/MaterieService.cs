using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services.ModelService
{
    class MaterieService
    {
        public static async Task<List<string>> getMaterie()
        {
            return await MaterieRepository.getAllMaterie(Constants.TutoripEndPoint + "/insegnante/findInsegnanteById.php/");
        }

        public static async Task<Materia[]> getMaterieInsegnante(int id)
        {
            return await MaterieRepository.getMaterieInsegnante(id , Constants.TutoripEndPoint + "/materia/findMaterieByIdInsegnante.php/");
        }
    }
}
