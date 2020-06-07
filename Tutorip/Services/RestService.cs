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
    }
}
