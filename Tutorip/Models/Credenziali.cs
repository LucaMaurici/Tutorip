using System;
using System.Collections.Generic;
using System.Text;
using Tutorip.GoogleAuthentication.Services;

namespace Tutorip.Models
{
    class Credenziali
    {
        public string Email { get; set; }
        public GoogleOAuthToken Token { get; set; }

        public Credenziali(string email, GoogleOAuthToken token){
            this.Email = email;
            this.Token = token;
        }
        
    }
}
