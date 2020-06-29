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
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

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
    }
}