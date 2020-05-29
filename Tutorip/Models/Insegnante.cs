using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorip.Models;

namespace tutoripProva.Models
{
    public class Insegnante
    {
        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("descrizione")]
        public String descrizione { get; set; }

        [JsonProperty("tariffa")]
        public float tariffa { get; set; }

        [JsonProperty("valutazioneMedia")]
        public String valutazione { get; set; }

        [JsonProperty("posizione")]
        public Posizione posizione { get; set; }

        public override string ToString()
        {
            return email + " " + tariffa;
        }
    }
}