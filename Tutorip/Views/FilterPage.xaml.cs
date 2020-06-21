using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Tutorip.Models;
using Tutorip.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : PopupPage
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
        }

        private void torna_indietro(object sender, EventArgs e)
        {
            parent.Opacity = 1;
            PopupNavigation.Instance.PopAsync(true);
        }

        private async void bt_applica_Clicked(object sender, EventArgs e)
        {
            filtri.tariffaMassima = float.Parse(en_tariffa.Text);
            filtri.valutazioneMinima = float.Parse(en_valutazione.Text);
            filtri.posizione = (Posizione)await positionAdapter.calcolaPosizione();
            RisultatoRicercaInsegnanti[] elenco = await InsegnantiService.GetInsegnanti(filtri);
            if (elenco!=null)
            {
                insegnanti_list.IsVisible = true;
                insegnanti_list.ItemsSource = elenco;
            }
            else
            {
                insegnanti_list.IsVisible = false;
                Console.WriteLine("Nessun insegnante");
            }
            parent.Opacity = 1;
            await PopupNavigation.Instance.PopAsync(true);
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