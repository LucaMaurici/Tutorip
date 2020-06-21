using System;
using Xamarin.Forms;
using Tutorip.Models;
using Tutorip.Services;
using Rg.Plugins.Popup.Services;

namespace Tutorip.Views
{
    public partial class SearchPage : ContentPage
    {

        Filtri filtri;
        PositionAdapter positionAdapter;
        public SearchPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.filtri = new Filtri();
            positionAdapter = new PositionAdapter();
            filtri.setDefault();
            setLabelValue();
        }

        public async void setLabelValue()
        {
            Posizione pos = (Posizione) await positionAdapter.calcolaPosizione();
            if (pos != null)
            {
                lb_posizione.Text = pos.indirizzo;
            }
            else
            {
                lb_posizione.Text = "Impossibile accedere alla posizione del dispositivo";
            }
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            filtri.nomeMateria = en_materia.Text;

            filtri.posizione = (Posizione) await positionAdapter.calcolaPosizione();
            RisultatoRicercaInsegnanti[] insegnanti = await InsegnantiService.GetInsegnanti(filtri);
            foreach(RisultatoRicercaInsegnanti r in insegnanti)
                r.distanza = r.distanza.Substring(0, 6);
            if (insegnanti != null)
            {
                insegnanti_list.IsVisible = true;
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
                await Navigation.PushAsync(new ProfilePage(i));
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

        /*private void login_btn_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<INativePages>().StartPage();
        }*/

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
    }
}