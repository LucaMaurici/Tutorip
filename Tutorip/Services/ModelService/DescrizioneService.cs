using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services.ModelService
{
    public class DescrizioneService
    {
        public static void Save(List<SezioneProfilo> descrizione)
        {
            DescrizioneRepository.Save(descrizione, Constants.TutoripEndPoint + "/descrizione/create.php/");
        }

        public static async Task<SezioneProfilo[]> getDescrizione(int id)
        {
            return await DescrizioneRepository.getDescrizione(id, Constants.TutoripEndPoint + "/descrizione/getDescrizioneById.php/");
        }
    }
}
