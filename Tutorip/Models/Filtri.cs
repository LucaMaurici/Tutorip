using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    public class Filtri
    {
        public String nomeMateria { get; set; }
        public Posizione posizione { get; set; }
        public float tariffaMassima { get; set; }
        public float valutazioneMinima { get; set; }

        internal void setDefault()
        {
            tariffaMassima = 1000000;
            valutazioneMinima = 0;
        }
    }
}
