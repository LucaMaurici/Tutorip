using Android.Accounts;
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
            this.MenuItems.Add(new ElementoMenu("users1", "Account"));
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void vc_profiloInsegnante_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private void vc_preferiti_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            ElementoMenu em = (ElementoMenu)e.Item;
            if (em.testo == "Diventa insegnante")
            {
                if (Preferences.Get("isInsegnante", false) == true)
                    await Navigation.PushAsync(new ProfilePage2(new Insegnante())); //sarà da mettere l'insegnante corrente
                else
                {
                    if(Preferences.Get("id", null) ==null)
                        await Navigation.PushAsync(new AccountPage());
                    else
                       await Navigation.PushAsync(new EditProfilePage(new Insegnante()));
                }

            }
            else if (em.testo == "Insegnanti preferiti")
            {
                this.IsEnabled = false;
                await Navigation.PushAsync(new ProfilePage(new Insegnante()));
                this.IsEnabled = true;
            }

            else if (em.testo == "Account")
            {
                await Navigation.PushAsync(new AccountPage());
            }
        }
    }
}