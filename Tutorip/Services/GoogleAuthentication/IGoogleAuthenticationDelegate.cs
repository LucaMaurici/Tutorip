using System;

namespace Tutorip.GoogleAuthentication.Services
{
    public interface IGoogleAuthenticationDelegate
    {
        void OnAuthenticationCompleted(OAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}