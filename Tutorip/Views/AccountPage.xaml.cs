using System;
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
        PositionAdapter positionAdapter;
        public AccountPage()
        {
            InitializeComponent();
            pageLayout();
            positionAdapter = new PositionAdapter();
        }

        private void pageLayout()
        {
            if(Preferences.Get("id", null) != null)
            {
                setNameSpan();
                btn_stack.IsVisible = false;
            }
            else
            {
                name_frame.IsVisible = false;
                pos_frame.IsVisible = false;
            }
        }

        private void setNameSpan()
        {
            nameSpan.Text = Preferences.Get("nome", null) + " " + Preferences.Get("cognome", null);
        }

        private void LogGoogBtn_clicked(object sender, EventArgs e)
        {
            DependencyService.Get<INativePages>().StartPage();
        }

        private void LogFbBtn_clicked(object sender, EventArgs e)
        {

        }

        private async void PosBtn_clicked(object sender, EventArgs e)
        {
            Posizione p = await positionAdapter.Indirizzo2Posizione(pos_entry.Text);
            Preferences.Set("latitudine", p.latitudine.ToString());
            Preferences.Set("longitudine", p.longitudine.ToString());
            Preferences.Set("indirizzo", p.indirizzo.ToString());
        }
    }
}