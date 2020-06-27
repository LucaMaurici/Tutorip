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
using System.Threading.Tasks;
using Android.Views.TextService;

namespace Tutorip.Views
{
    public partial class SearchPage : ContentPage
    {
        List<Materia> materie = new List<Materia>();
        List<String> suggestions = new List<String>(); //deve essere di materia per fare la list view più carina
        Filtri filtri;
        PositionAdapter positionAdapter;
        public SearchPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.filtri = new Filtri();
            positionAdapter = new PositionAdapter();
            Task.Run(()=> { listInitializer(); }) ;
            filtri.setDefault();
            setPositionButtonTextValue();
        }

        public async void setPositionButtonTextValue()
        {
            this.btn_posizione.Text = "Calcolando la posizione..";
            Posizione pos = (Posizione) await positionAdapter.calcolaPosizione();
            if (pos != null)
            {
                btn_posizione.Text = pos.indirizzo;
                Preferences.Set("latitudineCorrente", pos.latitudine.ToString());
                Preferences.Set("longitudineCorrente", pos.longitudine.ToString());
                Preferences.Set("indirizzoCorrente", pos.indirizzo);
            }
            else
            {
                btn_posizione.Text = "Impossibile accedere alla posizione del dispositivo, tocca per aggiornare";
            }
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            filtri.nomeMateria = en_materia.Text;
            //filtri.posizione = (Posizione) await positionAdapter.calcolaPosizione();
            //filtri.posizione = await positionAdapter.Indirizzo2Posizione(Preferences.Get("indirizzoCorrente", null));
            Posizione pos = new Posizione();
            if(Preferences.Get("latitudineCorrente", null) == null && Preferences.Get("longitudineCorrente", null) == null)
            {
                await DisplayAlert("Per eseguire la ricerca è necessaria la tua posizione", "Accendi la posizione del dispositivo o inserisci un indirizzo", "Ok");
            }
            else
            {
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
                        if (r.valutazioneMedia == null || r.valutazioneMedia == "")
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

        private void en_materia_TextChanged(object sender, TextChangedEventArgs e)
        {
            //da aggiungere qualcosa per conciliare la ricerca sulla entry con i risultati della precedente ricerca => probabilmente un isVIsible
            insegnanti_list.IsVisible = false;
            ListaDiMaterie.ItemsSource = null;
            var keyword = en_materia.Text;
            if(keyword.Length == 0)
            {
                ListaDiMaterie.IsVisible = false;
            }
            else
                ListaDiMaterie.IsVisible = true;
            suggestions.Clear();
            
            //var suggestions = materie.Where(m => m.nome.ToLower().Contains(keyword.ToLower()));
            foreach(Materia m in materie)
            {
                Console.WriteLine("NOME: " + m.nome);
                Console.WriteLine("KEYWORD: " + keyword);
                if (m.nome.ToLower().Contains(keyword.ToLower()))
                {
                    suggestions.Add(m.nome); //deve essere m per fare più carina la cosa del binding
                }
            }
            ListaDiMaterie.ItemsSource = suggestions;
        }

        private void ListaDiMaterie_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            String m = (String) e.Item; ////deve essere castato a materia per fare più carina la cosa del binding
            //var materia = e.Item as string;
            en_materia.Text = m;
        }

        private void btn_posizione_Clicked(object sender, EventArgs e)
        {
            this.setPositionButtonTextValue();
        }

        private async Task listInitializer()
        {
            materie = await MaterieService.getMaterie();
        }
    }
}