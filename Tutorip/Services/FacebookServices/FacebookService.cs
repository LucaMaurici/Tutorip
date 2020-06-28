using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tutorip.Services.FacebookServices
{
    public class FacebookService
    {
        public async Task<string> GetEmailAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email&access_token={accessToken}");
            Console.WriteLine(json);
            var email = JsonConvert.DeserializeObject<FacebookEmail>(json);
            return email.Email;
        }
    }
}