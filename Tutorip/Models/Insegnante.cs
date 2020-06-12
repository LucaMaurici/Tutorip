using Newtonsoft.Json;
using System;

namespace Tutorip.Models
{
    public class Insegnante
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("descrizione")]
        public String descrizione { get; set; }

        [JsonProperty("tariffa")]
        public float tariffa { get; set; }

        [JsonProperty("valutazioneMedia")]
        public String valutazione { get; set; }

        [JsonProperty("posizione")]
        public Posizione posizione { get; set; }

        [JsonProperty("numeroValutazioni")]
        public int numeroValutazioni { get; set; }

        [JsonProperty("promozioni")]
        public int promozioni { get; set; }

        [JsonProperty("gruppo")]
        public int gruppo { get; set; }

        [JsonProperty("dataOraRegistrazione")]
        public DateTime dataOraRegistrazione { get; set; }

        public override string ToString()
        {
            return id + " " + tariffa;
        }
    }
}