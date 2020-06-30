using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreferitiPage : ContentPage
    {
        public PreferitiPage(RisultatoRicercaInsegnanti[] preferiti)
        {
            InitializeComponent();
            this.preferiti_list.ItemsSource = preferiti;
            if (preferiti==null)
                this.lb_errore.IsVisible = true;
        }

        private async void preferiti_list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            this.IsEnabled = false;
            RisultatoRicercaInsegnanti r = (RisultatoRicercaInsegnanti)e.Item;
            Insegnante i = await InsegnantiService.getInsegnante(r.id);
            if (i.id != 0)
            {
                await Navigation.PushAsync(new ProfilePage2(i, "preferiti"));
            }
            this.IsEnabled = true;
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
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
    }
}