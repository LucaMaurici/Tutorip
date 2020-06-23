using Forms9Patch.Elements.Popups.Core;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Filtri filtri;
        private ListView insegnanti_list;
        private Page parent;
        private PositionAdapter positionAdapter;

        public FilterPage(Filtri f, ListView insegnanti_list, Page parent)
        {
            InitializeComponent();
            this.filtri = f;
            this.insegnanti_list = insegnanti_list;
            this.parent = parent;
            this.positionAdapter = new PositionAdapter();
            sl_tariffa.Value = f.tariffaMassima;
            en_tariffa.Text = f.tariffaMassima.ToString();
            sl_valutazione.Value = f.valutazioneMinima;
            en_valutazione.Text = f.valutazioneMinima.ToString();
            sl_distanzaMax.Value = f.distanzaMassima;
            en_distanzaMax.Text = f.distanzaMassima.ToString();
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
        private async void bt_applica_Clicked(object sender, EventArgs e)
        {
            filtri.tariffaMassima = float.Parse(en_tariffa.Text);
            filtri.valutazioneMinima = float.Parse(en_valutazione.Text);
            filtri.distanzaMassima = float.Parse(en_distanzaMax.Text);
            //filtri.posizione = (Posizione)await positionAdapter.calcolaPosizione();
            if (Preferences.Get("latitudineDefault", null) != null && cb_posizione.IsChecked)
            {
                Console.WriteLine("1111");
                Posizione pos = new Posizione();
                pos.latitudine = double.Parse(Preferences.Get("latitudineDefault", null));
                pos.longitudine = double.Parse(Preferences.Get("longitudineDefault", null));
                pos.indirizzo = Preferences.Get("indirizzoDefault", null);
                filtri.posizione = pos;
            }
            else
            {
                Console.WriteLine("2222");
                filtri.posizione = await positionAdapter.Indirizzo2Posizione(Preferences.Get("indirizzoCorrente", null));
            }

            RisultatoRicercaInsegnanti[] elenco = await InsegnantiService.GetInsegnanti(filtri);
            if (elenco!=null)
            {
                foreach (RisultatoRicercaInsegnanti r in elenco)
                    r.distanza = this.positionAdapter.approssimaDistanza(r.distanza);
                insegnanti_list.IsVisible = true;
                insegnanti_list.ItemsSource = elenco;
            }
            else
            {
                insegnanti_list.IsVisible = false;
                Console.WriteLine("Nessun insegnante");
            }
            parent.Opacity = 1;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
        }

        private void sl_tariffa_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            en_tariffa.Text = sl_tariffa.Value.ToString();
        }

        private void en_tariffa_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
                var tariffa = double.Parse(en_tariffa.Text);
                if (tariffa > 80)
                {
                    en_tariffa.Text = "80";
                }
                
                sl_tariffa.Value = tariffa;
            }
            catch
            {
                sl_tariffa.Value = 0;
                en_tariffa.Text = "0";
            }
        }

        private void sl_valutazione_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            en_valutazione.Text = sl_valutazione.Value.ToString();
        }

        private void en_valutazione_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var valutazione = double.Parse(en_valutazione.Text);
                if (valutazione > 10)
                {
                    en_valutazione.Text = "10";
                }
                sl_valutazione.Value = valutazione;
            }
            catch
            {
                sl_valutazione.Value = 0;
                en_valutazione.Text = "0";
            }
        }

        private void sl_distanzaMax_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            en_distanzaMax.Text = sl_distanzaMax.Value.ToString();
        }

        private void en_distanzaMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                var distanzaMax = double.Parse(en_distanzaMax.Text);
                if (distanzaMax > 50)
                {
                    en_distanzaMax.Text = "50";
                }
                
                sl_distanzaMax.Value = distanzaMax;
            }
            catch
            {
                sl_distanzaMax.Value = 0;
                en_distanzaMax.Text = "0";
            }
        }
    }
}