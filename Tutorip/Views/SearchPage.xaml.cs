using System;
using Xamarin.Forms;
using Tutorip.Models;
using Tutorip.Services;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using Tutorip.Services.ModelService;
using Android.Preferences;
using Xamarin.Essentials;

namespace Tutorip.Views
{
    public partial class SearchPage : ContentPage
    {
        List<Materia> materie = new List<Materia>();
        Filtri filtri;
        PositionAdapter positionAdapter;
        public SearchPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.filtri = new Filtri();
            this.listInitializr();
            positionAdapter = new PositionAdapter();
            filtri.setDefault();
            setButtonTextValue();
        }

        public async void setButtonTextValue()
        {
            this.btn_posizione.Text = "Tocca per aggiornare";
            Posizione pos = (Posizione) await positionAdapter.calcolaPosizione();
            if (pos != null)
            {
                btn_posizione.Text = pos.indirizzo;
            }
            else
            {
                btn_posizione.Text = "Impossibile accedere alla posizione del dispositivo";
            }
            Preferences.Set("latitudineCorrente", pos.latitudine.ToString());
            Preferences.Set("longitudineCorrente", pos.longitudine.ToString());
            Preferences.Set("indirizzoCorrente", pos.indirizzo);
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            filtri.nomeMateria = en_materia.Text;
            //filtri.posizione = (Posizione) await positionAdapter.calcolaPosizione();
            //filtri.posizione = await positionAdapter.Indirizzo2Posizione(Preferences.Get("indirizzoCorrente", null));
            Posizione pos = new Posizione();
            pos.latitudine = double.Parse(Preferences.Get("latitudineCorrente", null));
            pos.longitudine = double.Parse(Preferences.Get("longitudineCorrente", null));
            pos.indirizzo = Preferences.Get("indirizzoCorrente", null);
            filtri.posizione = pos;
            RisultatoRicercaInsegnanti[] insegnanti = await InsegnantiService.GetInsegnanti(filtri);
            if (insegnanti != null)
            {
                foreach (RisultatoRicercaInsegnanti r in insegnanti)
                {
                    r.distanza = this.positionAdapter.approssimaDistanza(r.distanza);
                    if(r.valutazioneMedia == null || r.valutazioneMedia=="")
                    {
                        //qualcosa per togliere la valutazione
                        r.valutazioneMedia = "//";
                    }
                }
                    
                insegnanti_list.IsVisible = true;
                ListaDiMaterie.IsVisible = false;
                insegnanti_list.ItemsSource = insegnanti;
            }
            else
            {
                insegnanti_list.IsVisible = false;
                Console.WriteLine("Nessun insegnante");
            }
        }

        private async void insegnanti_list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            this.IsEnabled = false;
            RisultatoRicercaInsegnanti r = (RisultatoRicercaInsegnanti)e.Item;
            Insegnante i = await InsegnantiService.getInsegnante(r.id);
            if (i.id != 0) 
            {
                await Navigation.PushAsync(new ProfilePage2(i));
            }
            this.IsEnabled = true;
        }

        private async void bt_filtri_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            filtri.nomeMateria = en_materia.Text;
            var page = new FilterPage(this.filtri, this.insegnanti_list, this);
            Opacity = 0.2;
            await PopupNavigation.Instance.PushAsync(page);
            this.IsEnabled = true;
        }

        private async void profile_btn_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PushAsync(new ProfilePage2(new Insegnante()));
            this.IsEnabled = true;
        }

        private async void bt_menu_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PushAsync(new MenuPage());
            this.IsEnabled = true;
        }

        /*private void en_materia_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = en_materia.Text;
            var suggestions = materie.Where(m => m.nome.ToLower().Contains(keyword.ToLower()));
            ListaDiMaterie.ItemsSource = suggestions;
        } NON FUNZIONA*/

        private void ListaDiMaterie_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            var materia = e.Item as string;
            en_materia.Text = materia;
        }

        private void btn_posizione_Clicked(object sender, EventArgs e)
        {
            this.setButtonTextValue();
        }

        private async void listInitializr()
        {
            materie = await MaterieService.getMaterie();
        }
    }
}