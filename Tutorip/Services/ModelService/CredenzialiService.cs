using System;
using System.Collections.Generic;
using System.Text;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Repository;

namespace Tutorip.Services
{
    public static class CredenzialiService
    {
        public static void Salva(Credenziali c)
        {
            CredenzialiRepository.Save(c, Constants.TutoripEndPoint + "/credenziali/create.php/");
        }

        public static void Esiste(Credenziali c)
        {
            CredenzialiRepository.Esiste(c, Constants.TutoripEndPoint + "/credenziali/create.php/");
        }
    }
}
