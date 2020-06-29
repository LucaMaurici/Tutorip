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
        }

        private void creaListaMenuItem()
        {
            this.MenuItems.Clear();
            if (Preferences.Get("isInsegnante", false))
                this.MenuItems.Add(new ElementoMenu("user6", "Profilo insegnante"));
            else
                this.MenuItems.Add(new ElementoMenu("user6", "Diventa insegnante"));
            this.MenuItems.Add(new ElementoMenu("bookmark", "Insegnanti salvati"));
            if (Preferences.Get("id", null) == null)
                this.MenuItems.Add(new ElementoMenu("users1", "Accedi o crea il tuo Account"));
            else
                this.MenuItems.Add(new ElementoMenu("users1", "Il tuo account"));
            Menu.ItemsSource = this.MenuItems;
        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();
            this.creaListaMenuItem();
        }*/

        private async void bt_indietro_Clicked(object sender, EventArgs e)
        {
            //Navigation.InsertPageBefore(new SearchPage(), this);
            //await Navigation.PopAsync();
            Navigation.PopToRootAsync();
            //Navigation.NavigationStack.GetEnumerator().MoveNext();
            //Navigation.RemovePage(Navigation.NavigationStack.GetEnumerator().Current);
        }

        protected override bool OnBackButtonPressed()
        {
            //Navigation.InsertPageBefore(new SearchPage(), this);
            //Navigation.PopAsync();
            Navigation.PopToRootAsync();
            return true;
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
                /*if (Preferences.Get("isInsegnante", false) == true)
                    await Navigation.PushAsync(new ProfilePage2(new Insegnante())); //sarà da mettere l'insegnante corrente
                else
                {*/
                if (Preferences.Get("id", null) == null)
                {
                    //await Navigation.PushAsync(new AccountPage());
                    Navigation.InsertPageBefore(new AccountPage(), this);
                    await Navigation.PopAsync();
                }
                else 
                {
                    Navigation.InsertPageBefore(new EditProfilePage(new Insegnante()), this);
                    await Navigation.PopAsync();
                }

            }

            else if (em.testo == "Profilo insegnante")
            {
                Insegnante i = await InsegnantiService.getInsegnante(int.Parse(Preferences.Get("id", (-1).ToString()))); //occhio al null
                if (i.id != 0)
                {
                    //Navigation.InsertPageBefore(new ProfilePage2(i, "menu"), this);
                    //await Navigation.PopAsync();
                    await Navigation.PushAsync(new ProfilePage2(i, "menu"));
                }
            }

            else if (em.testo == "Insegnanti preferiti")
            {
                this.IsEnabled = false;
                //await Navigation.PushAsync(new ProfilePage(new Insegnante()));
                if (Preferences.Get("id", null) != null)
                {
                    RisultatoRicercaInsegnanti[] risultati = await InsegnantiService.getPreferiti(int.Parse(Preferences.Get("id", null))); //eventualmente da spostare nella PreferitiPage
                    Navigation.InsertPageBefore(new PreferitiPage(risultati), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new AccountPage(), this);
                    await Navigation.PopAsync();
                }
                this.IsEnabled = true;
            }

            else if (em.testo == "Accedi o crea il tuo Account")
            {
                Navigation.InsertPageBefore(new AccountPage(), this);
                await Navigation.PopAsync();
            }

            else if(em.testo == "Il tuo account")
            {
                Navigation.InsertPageBefore(new UserAccountPage(), this);
                await Navigation.PopAsync();
            }
            //Navigation.InsertPageBefore(page, this);
            //await Navigation.PopAsync();
        }
    }
}