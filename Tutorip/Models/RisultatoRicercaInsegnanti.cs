using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    class RisultatoRicercaInsegnanti
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("valutazioneMedia")]
        public int? valutazioneMedia { get; set; }
        [JsonProperty("tariffa")]
        public int? tariffa { get; set; }
        [JsonProperty("distanza")]
        public String distanza { get; set; }

        public RisultatoRicercaInsegnanti() { }

        public String toString()
        {
            return this.tariffa.ToString() + " " + this.valutazioneMedia.ToString();
        }
    }
}
