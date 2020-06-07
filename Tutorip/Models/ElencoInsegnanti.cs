using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    class ElencoInsegnanti
    {
        [JsonProperty("ElencoRisultati")]
        public Insegnante[] Insegnanti { get; set; }
    }
}
