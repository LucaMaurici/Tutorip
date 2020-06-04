using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpTutorPage : ContentPage
    {
        public SignUpTutorPage()
        {
            InitializeComponent();
        }

        public static implicit operator Button(SignUpTutorPage v)
        {
            throw new NotImplementedException();
        }

        private void aggiungiImage_Clicked(object sender, EventArgs e)
        {

        }
    }
}