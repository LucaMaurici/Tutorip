﻿using System;
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
    public partial class SetPositionPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        //private Filtri filtri;
        private SearchPage parent;
        PositionAdapter positionAdapter;
        public SetPositionPage(Filtri f, Page parent)
        {
            InitializeComponent();
            //this.filtri = f;
            this.parent = (SearchPage) parent;
            this.positionAdapter = new PositionAdapter();
            this.en_indirizzo.Text = Preferences.Get("indirizzoDefault", null);
        }

        private async void btn_posAct_Clicked(object sender, EventArgs e)
        {
            //Posizione pos = (Posizione)await positionAdapter.calcolaPosizione();
            /*if (pos != null)
            {*/
            Preferences.Set("isUsingCurrentPos", "si");
            /*Posizione pos = new Posizione();
            pos.latitudine = double.Parse(Preferences.Get("latitudineCorrente", null));
            pos.longitudine = double.Parse(Preferences.Get("longitudineCorrente", null));
            pos.indirizzo = Preferences.Get("indirizzoCorrente", null);*/
            parent.GetPositionButton().Text = Preferences.Get("indirizzoCorrente", "");
            //parent.filtri.posizione = pos;
                /*Preferences.Set("latitudineCorrente", pos.latitudine.ToString());
                Preferences.Set("longitudineCorrente", pos.longitudine.ToString());
                Preferences.Set("indirizzoCorrente", pos.indirizzo);*/
            //}
            parent.Opacity = 1;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
        }

        private async void btn_indirizzo_Clicked(object sender, EventArgs e)
        {
            Posizione pos = await this.positionAdapter.Indirizzo2Posizione(this.en_indirizzo.Text);
            if (pos != null)
            {
                parent.GetPositionButton().Text = pos.indirizzo;
                //parent.filtri.posizione = pos;
                Preferences.Set("isUsingCurrentPos", "no");
                Preferences.Set("latitudineDefault", pos.latitudine.ToString());
                Preferences.Set("longitudineDefault", pos.longitudine.ToString());
                Preferences.Set("indirizzoDefault", pos.indirizzo);
            }
            parent.Opacity = 1;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
        }

        private void torna_indietro(object sender, EventArgs e)
        {
            parent.Opacity = 1;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
        }

        protected override bool OnBackButtonPressed()
        {
            parent.Opacity = 1;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
            return true;
        }
    }
}