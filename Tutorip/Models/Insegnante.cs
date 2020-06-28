using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tutorip.Models
{
    public class Insegnante
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("nomeDaVisualizzare")]
        public String nomeDaVisualizzare { get; set; }

        //immagine

        /*[JsonProperty("descrizione")]
        public String descrizione { get; set; }*/

        [JsonProperty("tariffa")]
        public float? tariffa { get; set; }

        [JsonProperty("valutazioneMedia")]
        public String valutazioneMedia { get; set; }

        [JsonProperty("numeroValutazioni")]
        public int? numeroValutazioni { get; set; }

        /*[JsonProperty("promozioni")]
        public int promozioni { get; set; }*/

        [JsonProperty("gruppo")]
        public int? gruppo { get; set; }

        /*[JsonProperty("dataOraRegistrazione")]
        public DateTime dataOraRegistrazione { get; set; }*/

        [JsonProperty("profiloPubblico")]
        public int? profiloPubblico { get; set; }

        [JsonProperty("contatti")]
        public Contatti contatti { get; set; }

        [JsonProperty("materie")]
        public List<Materia> materie { get; set; }

        [JsonProperty("posizione")]
        public Posizione posizione { get; set; }

        [JsonProperty("recensioni")]
        public List<Recensione> recensioni { get; set; }

        [JsonProperty("modalita")]
        public int? modalita { get; set; }

        [JsonProperty("Descrizione")]
        public List<SezioneProfilo> descrizione { get; set; }

        public Insegnante()
        {
            this.posizione = new Posizione();
            this.contatti = new Contatti();
        }
        public override string ToString()
        {
            return id + " " + tariffa;
        }
    }
}