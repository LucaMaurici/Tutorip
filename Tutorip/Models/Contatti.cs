using Newtonsoft.Json;
using System;

namespace Tutorip.Models
{
    public class Contatti
    {
        [JsonProperty("emailContatto")]
        public String emailContatto { get; set; }

        [JsonProperty("cellulare")]
        public String cellulare { get; set; }

        //[JsonProperty("facebook")]
        //String facebook { get; set; }
    }
}
