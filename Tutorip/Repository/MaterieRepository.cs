﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;

namespace Tutorip.Repository
{
    class MaterieRepository
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<Materia[]> getAllMaterie(string uri)
        {
            ElencoMaterie listaDiMaterie = null;
            var json = "{\"foo\":\" \"}";
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, sendContent);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    listaDiMaterie = JsonConvert.DeserializeObject<ElencoMaterie>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            if (listaDiMaterie != null)
            {
                return listaDiMaterie.Risultati;
            }
            return listaDiMaterie.Risultati;
        }

        public static async Task<Materia[]> getMaterieInsegnante(int id, string uri)
        {
            ElencoMaterie elenco = null;
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
                    elenco = JsonConvert.DeserializeObject<ElencoMaterie>(content);
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

        internal class ElencoMaterie
        {
            [JsonProperty("ElencoMaterie")]
            public Materia[] Risultati { get; set; }
        }
    }
}




