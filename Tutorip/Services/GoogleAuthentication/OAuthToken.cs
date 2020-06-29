namespace Tutorip.GoogleAuthentication.Services
{
    public class OAuthToken
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public OAuthToken(string TokenType, string AccessToken)
        {
            this.TokenType = TokenType;
            this.AccessToken = AccessToken;
        }
    }
}