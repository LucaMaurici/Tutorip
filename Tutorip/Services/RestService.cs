using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
using tutoripProva.Models;

namespace Tutorip.Services
{
    static class RestService
    {
        private static readonly HttpClient _client = new HttpClient();


        public static async Task<ElencoInsegnanti> GetInsegnantiDataAsync(Filtri filtri, string uri)
        {
            ElencoInsegnanti insegnanti = null;

            var json = JsonConvert.SerializeObject(filtri);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    insegnanti = JsonConvert.DeserializeObject<ElencoInsegnanti>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return insegnanti;
        }

        public static async void SaveElements(Credenziali credenziali, String uri, bool isNewItem = false)
        {
            var json = JsonConvert.SerializeObject(credenziali);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (isNewItem) //??
            {
                try
                {
                    response = await _client.PostAsync(uri, content);
                    Console.WriteLine(response.StatusCode);
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
    }
}
