using System;
using System.Collections.Generic;
using System.Text;
using Tutorip.Data;
using Tutorip.GoogleAuthentication.Services;
using Tutorip.Services;
using Xamarin.Essentials;

namespace Tutorip.Models
{
    public class Credenziali
    {
        public string Email { get; set; }
        public GoogleOAuthToken Token { get; set; }

        public Credenziali(string email, GoogleOAuthToken token){
            this.Email = email;
            this.Token = token;
        }
        
        public void salva()
        {
            RestService.SaveElements(this, Constants.TutoripEndPoint + "/credenziali/create.php/");
        }
    }
}
