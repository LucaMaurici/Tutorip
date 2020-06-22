using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tutorip.Repository
{
    class MaterieRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<List<string>> getAllMaterie(string uri)
        {
            List<string> listaDiMaterie = new List<string>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    listaDiMaterie = JsonConvert.DeserializeObject<List<string>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return listaDiMaterie;
        }
    }
}




