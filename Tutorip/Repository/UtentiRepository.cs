using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;

namespace Tutorip.Repository
{
    public class UtentiRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        public static async void SaveAsync(Utente u, string uri)
        {
            var json = JsonConvert.SerializeObject(u);
            Console.WriteLine("Json: " + json);
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
    }
}
