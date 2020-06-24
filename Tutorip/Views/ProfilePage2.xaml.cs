﻿using System;
using System.Collections.Generic;
using Tutorip.Models;
using Tutorip.Services;
using Tutorip.Services.ModelService;
using Xamarin.Essentials;
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
            this.btn_salvaRecensione.IsVisible = false;
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
            this.subject_list.ItemsSource = insegnante.materie;
        }

        private void bt_indietro_Clicked(object sender, System.EventArgs e)
        {
            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id)
            {
                Navigation.InsertPageBefore(new MenuPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id) {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            }   
            else
            {
                Navigation.PopAsync();
            }
            return true;
        }

        private async void bt_ModificaProfilo_Clicked(object sender, System.EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PushAsync(new EditProfilePage(this.insegnante)); // conveniente pushare???
            this.IsEnabled = true;
        }

        private void bt_ModificaVisibilità_Clicked(object sender, System.EventArgs e)
        {

        }

        private void bt_addFav_Clicked(object sender, EventArgs e) // bottone per aggiungere ai preferiti da mostrare solo se si è utenti che vedono il profilo
        {
            InsegnantiService.aggiungiPreferito(int.Parse(Preferences.Get("id", null)), insegnante.id);
        }

        private void btn_recensione_Clicked(object sender, EventArgs e)
        {

            var entryTitolo = new Entry { Placeholder = "Titolo", Keyboard = Keyboard.Text };
            var entryCorpo =  new Entry { Placeholder = "Testo della recensione", Keyboard = Keyboard.Text };
            var entryValMed =  new Entry { Placeholder = "ValutazioneMedia", Keyboard = Keyboard.Numeric };
            var entryEmpatia =  new Entry { Placeholder = "Empatia", Keyboard = Keyboard.Numeric };
            var entrySpieg = new Entry { Placeholder = "Spiegazione", Keyboard = Keyboard.Numeric };
            var entryOrg = new Entry { Placeholder = "Organizzazione", Keyboard = Keyboard.Numeric };
            
            this.stl_recensione.Children.Add(entryTitolo);
            this.stl_recensione.Children.Add(entryCorpo);
            this.stl_recensione.Children.Add(entryValMed);
            this.stl_recensione.Children.Add(entryEmpatia);
            this.stl_recensione.Children.Add(entrySpieg);
            this.stl_recensione.Children.Add(entryOrg);

            this.btn_salvaRecensione.IsVisible = true;
        }

        private void btn_salvaRecensione_Clicked(object sender, EventArgs e)
        {
            Recensione r = new Recensione();

            r.cod_insegnante = insegnante.id;
            r.cod_utente = int.Parse(Preferences.Get("id", (-1).ToString()));

            if (this.cb_isAnonimo.IsChecked)
                r.anonimo = 1;
            else
                r.anonimo = 0;

            Entry entryTitolo = (Entry) stl_recensione.Children[0];
            r.titolo = entryTitolo.Text;
            Entry entryCorpo = (Entry)stl_recensione.Children[1];
            r.corpo = entryCorpo.Text;
            Entry entryValMed = (Entry)stl_recensione.Children[2];
            r.valutazioneGenerale = int.Parse(entryValMed.Text);
            Entry entryEmp = (Entry)stl_recensione.Children[3];
            r.empatia = int.Parse(entryEmp.Text);
            Entry entrySpieg = (Entry)stl_recensione.Children[4];
            r.spiegazione = int.Parse(entrySpieg.Text);
            Entry entryOrg = (Entry)stl_recensione.Children[5];
            r.organizzazione = int.Parse(entryOrg.Text);

            RecensioniService.Save(r);
        }
    }
}