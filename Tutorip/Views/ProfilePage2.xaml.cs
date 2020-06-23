using System;
using System.Collections.Generic;
using Tutorip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage2 : ContentPage
    {

        Insegnante insegnante;

        public ProfilePage2(Insegnante insegnante)
        {
            InitializeComponent();
            //name_lbl.Text = item.email;
            this.insegnante = insegnante;
            completeProfile();
        }

        private void completeProfile()
        {
            if (insegnante.gruppo == 1)
                this.lb_gruppo.Text = "Lezione di gruppo su richiesta";
            else
                this.lb_gruppo.Text = "Lezione in singolo";

            if (insegnante.modalita == 0)
                this.lb_mod.Text = "In presenza";
            if (insegnante.modalita == 1)
                this.lb_mod.Text = "A distanza";
            if (insegnante.modalita == 2)
                this.lb_mod.Text = "In presenza e a distanza";

            this.distanza_lbl.Text = insegnante.posizione.indirizzo;
            this.lb_email.Text = insegnante.contatti.emailContatto;
            this.eval_lbl.Text = insegnante.valutazioneMedia;
            this.name_lbl.Text = insegnante.nomeDaVisualizzare;
            this.tariffa_spn.Text = insegnante.tariffa.ToString();
            
            //List<string> materie = new List<Materia>();
            //foreach (Materia m in insegnante.materie)
            //    materie.Add(m);
            this.subject_list.ItemsSource = insegnante.materie;
        }

        private void bt_indietro_Clicked(object sender, System.EventArgs e)
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            return true;
        }

        private async void bt_ModificaProfilo_Clicked(object sender, System.EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PushAsync(new EditProfilePage(this.insegnante));
            this.IsEnabled = true;
        }

        private void bt_ModificaVisibilità_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}