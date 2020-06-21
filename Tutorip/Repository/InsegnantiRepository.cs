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
            Console.WriteLine(json);
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    Console.WriteLine("PROVA3");
                    elenco = JsonConvert.DeserializeObject<ElencoInsegnanti>(content);
                    Console.WriteLine("PROVA");
                    Console.WriteLine("ARRAY: " + elenco.Insegnanti);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("PROVA2");
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            if(elenco != null)
                return elenco.Insegnanti;
            return new Insegnante[0];
        }

        public static async Task SaveAsync(Insegnante i, string uri)
        {
            var json = JsonConvert.SerializeObject(i);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch
            {
                Console.WriteLine("errore");
            }
        }

        internal class ElencoInsegnanti
        {
            [JsonProperty("ElencoRisultati")]
            public Insegnante[] Insegnanti { get; set; }
        }

    }
}
