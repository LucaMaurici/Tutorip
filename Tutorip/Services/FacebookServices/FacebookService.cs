using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tutorip.Services.FacebookServices
{
    public class FacebookService
    {
        public async Task<FacebookProfile> GetEmailAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name&access_token={accessToken}");
            Console.WriteLine(json);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(json);
            Console.WriteLine("PORCO");
            Console.WriteLine(facebookProfile);
            return facebookProfile;
        }
    }
}