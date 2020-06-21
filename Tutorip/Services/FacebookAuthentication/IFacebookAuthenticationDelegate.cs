using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Tutorip.Services.FacebookAuthentication
{
    public interface IFacebookAuthenticationDelegate
    {
        void OnAuthenticationCompleted(FacebookOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}