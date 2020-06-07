using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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
            if (isNewItem) //??
            {
                try
                {
                    response = await _client.PostAsync(uri, content);
                    Console.WriteLine("=====================================================");
                    //Console.WriteLine(response.StatusCode);
                    //Console.WriteLine(response);
                    Console.WriteLine("JSON : " + json);
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

        internal static void Esiste(Credenziali c, string v)
        {
            throw new NotImplementedException();
        }
    }
}
