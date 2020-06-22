using System;

namespace Tutorip.Models
{
    public class Filtri
    {
        //COMMENTO FASULLO
        public String nomeMateria { get; set; }
        public Posizione posizione { get; set; }
        public float tariffaMassima { get; set; }
        public float valutazioneMinima { get; set; }
        public float distanzaMassima { get; set; }

        internal void setDefault()
        {
            tariffaMassima = 80;
            valutazioneMinima = 0;
            distanzaMassima = 10000000;
        }
    }
}
