using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountPage : ContentPage
    {
        PositionAdapter positionAdapter;
        public UserAccountPage()
        {
            InitializeComponent();
            this.positionAdapter = new PositionAdapter();
            setNameSpan();
        }

        private void setNameSpan()
        {
            nameSpan.Text = Preferences.Get("nome", null) + " " + Preferences.Get("cognome", null);
        }

        private async void PosBtn_clicked(object sender, EventArgs e)
        {
            Posizione p = await positionAdapter.Indirizzo2Posizione(pos_entry.Text);
            Preferences.Set("latitudineDefault", p.latitudine.ToString());
            Preferences.Set("longitudineDefault", p.longitudine.ToString());
            Preferences.Set("indirizzoDefault", p.indirizzo);
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            return true;
        }

        private void btn_logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("id");
            Preferences.Remove("nome");
            Preferences.Remove("cognome");
            Preferences.Remove("email");
            Preferences.Remove("accessToken");
            Preferences.Remove("tokenType");
            Preferences.Remove("isInsegnante");
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
        }
    }
}