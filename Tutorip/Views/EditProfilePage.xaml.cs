using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        private Insegnante insegnante;
        private PositionAdapter positionAdapter;

        public EditProfilePage(Insegnante insegnante)
        {
            InitializeComponent();
            this.insegnante = insegnante;
            this.positionAdapter = new PositionAdapter();
        }

        private async void bt_SalvaProfilo_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            insegnante.nomeDaVisualizzare = this.en_nome.Text;
            insegnante.tariffa = float.Parse(this.en_tariffa.Text);
            if (this.cb_gruppo.IsChecked)
                insegnante.gruppo = 1;
            else
                insegnante.gruppo = 0;
            insegnante.posizione = await positionAdapter.Indirizzo2Posizione(en_indirizzo.Text);
            this.IsEnabled = true;
            //insegnante.dataOraRegistrazione = DateTime.Now;
            insegnante.id = int.Parse(Preferences.Get("id", (-1).ToString()));
            if (insegnante.id != -1)
                InsegnantiService.Save(insegnante);
            else
                Console.WriteLine("Errore");
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void img_profileImage_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }
    }
}