using System;
using System.Collections.Generic;
using System.Text;
using Tutorip.GoogleAuthentication.Services;

namespace Tutorip.Models
{
    class Credenziali
    {
        public Credenziali(String email, GoogleOAuthToken token){
            this.email = email;
            this.token = token;
        }
        String email { get; set; }
        GoogleOAuthToken token { get; set; }
    }
}
