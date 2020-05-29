using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tutorip.Models;
using Tutorip.Views;
using Tutorip.Services;
using Tutorip.Data;
using tutoripProva.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Tutorip.Views
{
    public partial class SearchPage : ContentPage
    {

        Filtri filtri;
        Location location;

        public SearchPage()
        {
            InitializeComponent();
            this.filtri = new Filtri();
            filtri.setDefault();
            calculatePosition();
        }

        public async void calculatePosition()
        {
            location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            Geocoder geoCoder = new Geocoder();

            Position position = new Position(location.Latitude, location.Longitude);
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
            string address = possibleAddresses.FirstOrDefault();
            lb_posizione.Text = address;
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            filtri.nomeMateria = en_materia.Text;
            Console.WriteLine("prova1");

            Posizione p = new Posizione
            {
                latitudine = location.Latitude,
                longitudine = location.Longitude,
                indirizzo = "via dei cabibbi 18"
            };

            filtri.posizione = p;
            Console.WriteLine("prova2");

            ElencoInsegnanti elenco = await RestService.GetInsegnantiDataAsync(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
            if (elenco != null)
            {
                Console.WriteLine("prova3");
                insegnanti_list.IsVisible = true;
                insegnanti_list.ItemsSource = elenco.Insegnanti;
            }
            else
            {
                insegnanti_list.IsVisible = false;
                Console.WriteLine("Nessun insegnante");
            }
        }

        private void insegnanti_list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ProfilePage((Insegnante)e.Item));
        }

        private async void bt_filtri_Clicked(object sender, EventArgs e)
        {
            filtri.nomeMateria = en_materia.Text;
            //var page = new FilterPage(this.filtri, this.insegnanti_list, this);
            Opacity = 0.2;
            //await PopupNavigation.Instance.PushAsync(page);
        }
    }
}