using Tutorip.GoogleAuthentication.Services;

namespace Tutorip.Models
{
    public class Credenziali
    {
        public string Email { get; set; }
        public OAuthToken Token { get; set; }

        public Credenziali(string email, OAuthToken token){
            this.Email = email;
            this.Token = token;
        }
    }
}
