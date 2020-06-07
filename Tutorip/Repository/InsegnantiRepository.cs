using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;

namespace Tutorip.Repository
{
    class InsegnantiRepository
    {

        private static readonly HttpClient _client = new HttpClient();

        public static async Task<Insegnante[]> GetInsegnanti(Filtri filtri, string uri)
        {
            ElencoInsegnanti elenco = null;

            var json = JsonConvert.SerializeObject(filtri);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    elenco = JsonConvert.DeserializeObject<ElencoInsegnanti>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            if(elenco != null)
                return elenco.Insegnanti;
            return new Insegnante[0];
        }

        internal class ElencoInsegnanti
        {
            [JsonProperty("ElencoRisultati")]
            public Insegnante[] Insegnanti { get; set; }
        }

    }
}
