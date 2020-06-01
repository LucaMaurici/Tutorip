using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorip.Data;
using Tutorip.Models;
using Tutorip.Services;
using tutoripProva.Models;
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

        public FilterPage(Filtri f, ListView insegnanti_list, Page parent)
        {
            InitializeComponent();
            this.filtri = f;
            this.insegnanti_list = insegnanti_list;
            this.parent = parent;
            sl_tariffa.Value = f.tariffaMassima;
            en_tariffa.Text = f.tariffaMassima.ToString();
        }

        private void bt_annulla_Clicked(object sender, EventArgs e)
        {
            //Navigation.PopAsync();
            parent.Opacity = 1;
            PopupNavigation.Instance.PopAsync(true);
        }

        /*
        private async void bt_applica_ClickedAsync(object sender, EventArgs e)
        {
            filtri.tariffaMassima = float.Parse(en_tariffa.Text);
            filtri.valutazioneMinima = float.Parse(en_valutazione.Text);
            
            ElencoInsegnanti elenco = await RestService.GetInsegnantiDataAsync(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
            if (elenco != null)
            {
                insegnanti_list.IsVisible = true;
                insegnanti_list.ItemsSource = elenco.Insegnanti;
            }
            else
            {
                insegnanti_list.IsVisible = false;
                Console.WriteLine("Nessun insegnante");
            }
            await Navigation.PopAsync();
        }*/

        private async void bt_applica_Clicked(object sender, EventArgs e)
        {
            filtri.tariffaMassima = float.Parse(en_tariffa.Text);
            filtri.valutazioneMinima = float.Parse(en_valutazione.Text);

            ElencoInsegnanti elenco = await RestService.GetInsegnantiDataAsync(filtri, Constants.TutoripEndPoint + "/ricerca/ricerca.php/");
            if (elenco != null)
            {
                insegnanti_list.IsVisible = true;
                insegnanti_list.ItemsSource = elenco.Insegnanti;
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
            //sl_tariffa.Value = Math.Round(sl_tariffa.Value);
            en_tariffa.Text = sl_tariffa.Value.ToString();
        }

        private void en_tariffa_TextChanged(object sender, TextChangedEventArgs e)
        {
            //en_tariffa.Text = Math.Round(double.Parse(en_tariffa.Text)).ToString();
            /*
            if(en_tariffa.Text.Contains(","))
            {
                en_tariffa.Text = en_tariffa.Text.Substring(0, en_tariffa.Text.IndexOf(","));
            }*/
            sl_tariffa.Value = double.Parse(en_tariffa.Text);
        }


    }
}