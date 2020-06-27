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
                    Console.WriteLine(content);
                    i = JsonConvert.DeserializeObject<Insegnante>(content);
                }
            }
            catch
            {
                Console.WriteLine("Errore");
            }
            return i;
        }

        internal static async Task<RisultatoRicercaInsegnanti[]> getPreferiti(int idUtente, string uri)
        {
            ElencoInsegnanti elenco = null;
            var json = "{\"idUtente\":" + JsonConvert.SerializeObject(idUtente) + "}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("JSON: " + json);
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

        public static async void aggiungiPreferito(int cod_utente, int cod_insegnante, string uri)
        {
            var json = "{\"cod_utente\":" + JsonConvert.SerializeObject(cod_utente) + "," + "\"cod_insegnante\":" + JsonConvert.SerializeObject(cod_insegnante) + "}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("JSON: " + json);
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, sendContent);
            }
            catch
            {
                Console.WriteLine("ERRORE");
            }
        }

        public static async Task<int> SaveAsync(Insegnante i, string uri) //Bisogna far si che in caso già esista il profilo viene aggiornato
        {
            temp result = new temp();
            var json = JsonConvert.SerializeObject(i);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<temp>(content);
                }
            }
            catch
            {
                Console.WriteLine("errore");
            }
            if(result != null)
            {
                return result.n;
            }
            return -1;
        }

        internal class ElencoInsegnanti
        {
            [JsonProperty("ElencoRisultati")]
            public RisultatoRicercaInsegnanti[] Risultati { get; set; }
        }

        internal class temp
        {
            public int n;
        }

    }
}
