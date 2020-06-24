using Newtonsoft.Json;
using System;

namespace Tutorip.Models
{
    public class Recensione
    {
        /*[JsonProperty("nomeAutore")]
        public String nome { get; set; }

        [JsonProperty("cognomeAutore")]
        public String cognome { get; set; }*/

        [JsonProperty("utente")]
        public Utente utente { get; set; }
        
        [JsonProperty("cod_utente")]
        public int? cod_utente { get; set; }

        [JsonProperty("cod_insegnante")]
        public int? cod_insegnante { get; set; }

        [JsonProperty("titolo")]
        public String titolo { get; set; }

        [JsonProperty("corpo")]
        public String corpo { get; set; }

        [JsonProperty("valutazioneGenerale")]
        public int? valutazioneGenerale { get; set; }

        [JsonProperty("empatia")]
        public int? empatia { get; set; }

        [JsonProperty("spiegazione")]
        public int? spiegazione { get; set; }

        [JsonProperty("organizzazione")]
        public int? organizzazione { get; set; }

        [JsonProperty("anonimo")]
        public int? anonimo { get; set; }

    }
}
