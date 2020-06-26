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
    public class DescrizioneRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async void Save(List<SezioneProfilo> descrizione, string uri)
        {
            var json = JsonConvert.SerializeObject(descrizione);
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

        public static async Task<SezioneProfilo[]> getDescrizione(int id, string uri)
        {
            Descrizione descrizione = null;
            var json = "{\"id\":" + JsonConvert.SerializeObject(id) + "}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    descrizione = JsonConvert.DeserializeObject<Descrizione>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            if (descrizione != null)
            {
                return descrizione.Risultati;
            }
            return null;
        }

        internal class Descrizione
        {
            [JsonProperty("descrizione")]
            public SezioneProfilo[] Risultati { get; set; }
        }
    }
}
