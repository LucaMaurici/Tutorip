using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;

namespace Tutorip.Repository
{
    class RecensioniRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async void Save(Recensione recensione, string uri)
        {
            var json = JsonConvert.SerializeObject(recensione);
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

        public static async Task<Recensione[]> GetRecensioniInsegnante(int idInsegnante, string uri)
        {
            ElencoRecensioni elenco = null;
            var json = "{\"id\":" + JsonConvert.SerializeObject(idInsegnante) + "}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    elenco = JsonConvert.DeserializeObject<ElencoRecensioni>(content);
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

        internal class ElencoRecensioni
        {
            [JsonProperty("ElencoRisultati")]
            public Recensione[] Risultati { get; set; }
        }
    }
}
