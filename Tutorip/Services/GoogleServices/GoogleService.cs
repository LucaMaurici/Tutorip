using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tutorip.Services.GoogleServices
{
    public class GoogleService
    {
        public async Task<GoogleProfile> GetProfileAsync(string tokenType, string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
            var json = await httpClient.GetStringAsync("https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + accessToken);
            System.Console.WriteLine(json);
            var profile = JsonConvert.DeserializeObject<GoogleProfile>(json);
            return profile;
        }
    }
}
