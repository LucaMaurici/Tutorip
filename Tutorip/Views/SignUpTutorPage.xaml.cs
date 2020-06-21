using System;
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

        private async void aggiungiImage_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }
    }
}