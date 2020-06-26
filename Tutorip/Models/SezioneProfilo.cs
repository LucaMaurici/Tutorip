using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    public class SezioneProfilo
    {
        [JsonProperty("id")]
        public int? id { get; set; }

        [JsonProperty("indice")]
        public int? indice { get; set; }

        [JsonProperty("idInsegnante")]
        public int? idInsegnante { get; set; }

        [JsonProperty("titolo")]
        public String titolo { get; set; }

        [JsonProperty("corpo")]
        public String corpo { get; set; }

    }
}
