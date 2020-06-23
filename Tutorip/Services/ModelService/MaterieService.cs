using Java.Util;
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
        public static async Task<List<Materia>> getMaterie()
        {
            Materia[] fakeMaterieArray = await MaterieRepository.getAllMaterie(Constants.TutoripEndPoint + "/materia/findAllMaterie.php/");
            List<Materia> listaDiMaterie = new List<Materia>();
            foreach(Materia m in fakeMaterieArray) {
                    listaDiMaterie.Add(m);
                }
            return listaDiMaterie;
        }

        public static async Task<Materia[]> getMaterieInsegnante(int id)
        {
            return await MaterieRepository.getMaterieInsegnante(id , Constants.TutoripEndPoint + "/materia/findMaterieByIdInsegnante.php/");
        }
    }
}
