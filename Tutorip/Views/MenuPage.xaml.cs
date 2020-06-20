using System;
using System.Collections.Generic;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        List<ElementoMenu> MenuItems;
        public MenuPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.MenuItems = new List<ElementoMenu>();
            creaListaMenuItem();
            Menu.ItemsSource = this.MenuItems;
        }

        private void creaListaMenuItem()
        {
            this.MenuItems.Add(new ElementoMenu("user6", "Diventa insegnante"));
            this.MenuItems.Add(new ElementoMenu("star1", "Insegnanti preferiti"));
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void vc_profiloInsegnante_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private async void vc_preferiti_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            ElementoMenu em = (ElementoMenu)e.Item;
            if(em.testo == "Diventa insegnante")
            {
                Insegnante i = new Insegnante();
                i.nomeDaVisualizzare = "gino";
                i.profiloPubblico = 1;
                i.id = int.Parse(Preferences.Get("id", null));
                Console.WriteLine("prova1");
                InsegnantiService.Save(i);
                //Navigation.PushAsync(new SignUpTutorPage());
            }
            else if (em.testo == "Insegnanti preferiti")
            {
                this.IsEnabled = false;
                Navigation.PushAsync(new ProfilePage(new Insegnante()));
                this.IsEnabled = true;
            }
        }
    }
}