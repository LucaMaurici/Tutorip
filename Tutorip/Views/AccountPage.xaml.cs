using Java.Lang;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        List<ElementoMenu> MenuItems;
        public AccountPage()
        {
            InitializeComponent();
            this.MenuItems = new List<ElementoMenu>();
            creaListaMenuItem();
        }

        private void creaListaMenuItem()
        {
            this.MenuItems.Clear();
            this.MenuItems = new List<ElementoMenu>();
            this.MenuItems.Add(new ElementoMenu("google", "Accedi con Google"));
            this.MenuItems.Add(new ElementoMenu("facebook", "Accedi con Facebook"));
            PossibleLog.ItemsSource = this.MenuItems;
        }

        /*private async void LogGoogBtn_clicked(object sender, EventArgs e)
        {
            await Task.Run(() => { DependencyService.Get<INativePages>().StartPage(); });
            Navigation.InsertPageBefore(new WelcomePage(), this);
            await Navigation.PopAsync();
        }

        private void LogFbBtn_clicked(object sender, EventArgs e)
        {
            DependencyService.Get<INativePages>().FacebookStartPage();
        }*/



        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            //return base.OnBackButtonPressed();
            return true;
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
        }

        private async void PossibleLog_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            ElementoMenu em = (ElementoMenu)e.Item;
            if (em.testo == "Accedi con Google")
            {
                await Task.Run(() => { DependencyService.Get<INativePages>().StartPage(); });
                Navigation.InsertPageBefore(new WelcomePage(), this);
                await Navigation.PopAsync();
            }
            else if (em.testo == "Accedi con Facebook")
            {
                await Task.Run(() => { DependencyService.Get<INativePages>().FacebookStartPage(); });
                Navigation.InsertPageBefore(new WelcomePage(), this);
                await Navigation.PopAsync();
            }
                
        }
    }
}