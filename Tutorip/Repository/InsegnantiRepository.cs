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

        public static async Task<RisultatoRicercaInsegnanti[]> GetInsegnanti(Filtri filtri, string uri)
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
                    elenco = JsonConvert.DeserializeObject<ElencoInsegnanti>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            if (elenco != null)
            {
                return elenco.Risultati;
            }
            return null;
        }

        internal static async Task<Insegnante> findInsegnanteById(int id, string uri)
        {
            Insegnante i = new Insegnante();
            var json = "{\"id\":" + JsonConvert.SerializeObject(id) +"}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("JSON: "+ json);
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    i = JsonConvert.DeserializeObject<Insegnante>(content);
                }
            }
            catch
            {
                Console.WriteLine("Errore");
            }
            return i;
        }

        public static async Task SaveAsync(Insegnante i, string uri) //Bisogna far si che in caso già esista il profilo viene aggiornato
        {
            var json = JsonConvert.SerializeObject(i);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
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
            public RisultatoRicercaInsegnanti[] Risultati { get; set; }
        }

    }
}
