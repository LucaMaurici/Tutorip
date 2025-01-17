﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    public class RisultatoRicercaInsegnanti
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("nomeDaVisualizzare")]
        public String nomeDaVisualizzare { get; set; }

        [JsonProperty("valutazioneMedia")]
        public String valutazioneMedia { get; set; }

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
