namespace Tutorip.GoogleAuthentication.Services
{
    public class GoogleOAuthToken
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public GoogleOAuthToken(string TokenType, string AccessToken)
        {
            this.TokenType = TokenType;
            this.AccessToken = AccessToken;
        }
    }
}