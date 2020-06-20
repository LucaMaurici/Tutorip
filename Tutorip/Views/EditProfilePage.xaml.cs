using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        private Insegnante insegnante;

        public EditProfilePage(Insegnante insegnante)
        {
            InitializeComponent();
            this.insegnante = insegnante;
        }

        private void bt_SalvaProfilo_Clicked(object sender, EventArgs e)
        {

        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}