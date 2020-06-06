using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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
    }
}