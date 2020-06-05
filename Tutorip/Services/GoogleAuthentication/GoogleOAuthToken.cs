namespace Tutorip.GoogleAuthentication.Services
{
    public class GoogleOAuthToken
    {
        public GoogleOAuthToken(string TokenType, string AccessToken)
        {
            this.TokenType = TokenType;
            this.AccessToken = AccessToken;
        }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
    }
}