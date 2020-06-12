using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;

namespace Tutorip.Repository
{
    class CredenzialiRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async void Save(Credenziali credenziali, String uri, bool isNewItem = true)
        {
            var json = JsonConvert.SerializeObject(credenziali);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (isNewItem)
            {
                try
                {
                    response = await _client.PostAsync(uri, content);
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine("\tERROR {1}", ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\tERROR {0}", ex.Message);
                }
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved."); //Non utile
            }
        }

        public static async Task<bool> Esiste(Credenziali c, string v)
        {
            bool esito = false;
            var json = "{\"Email\": " + JsonConvert.SerializeObject(c.Email) +"}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(v, sendContent);
                Console.WriteLine(response.StatusCode);
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    temp result = JsonConvert.DeserializeObject<temp>(content);
                    
                    Console.WriteLine("ESITO: " + result);
                    if (result.n==0)
                    {
                        esito = true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERRORE");
                Debug.WriteLine("\tERROR {1}", ex.Message);
            }
            return esito;
        }

        public static async Task<int> getIdAsync(string email, string uri)
        {
            int id = 0;
            var json = "{\"Email\": " + JsonConvert.SerializeObject(email) + "}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, sendContent);
                Console.WriteLine(response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    temp result = JsonConvert.DeserializeObject<temp>(content);
                    id = result.n;
                }
            }
            catch
            {
                Console.WriteLine("ERRORE");
            }
            return id;
        }

        internal class temp
        {
            public int n;
        }
    }
}
