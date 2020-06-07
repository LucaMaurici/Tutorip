using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
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

        private void vc_profiloInsegnante_Tapped(object sender, EventArgs e)
        {

        }

        private void vc_preferiti_Tapped(object sender, EventArgs e)
        {

        }

        private void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
                lv.SelectedItem = null;
            ElementoMenu em = (ElementoMenu)e.Item;
            if(em.testo == "Diventa insegnante")
            {
                Navigation.PushAsync(new ProfilePage(new Insegnante()));
            }
            else if (em.testo == "Insegnanti preferiti")
            {
                Navigation.PushAsync(new ProfilePage(new Insegnante()));
            }
        }
    }
}