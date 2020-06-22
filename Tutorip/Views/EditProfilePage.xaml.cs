using System;
using System.Collections;
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
    public partial class EditProfilePage : ContentPage
    {
        private Insegnante insegnante;
        private PositionAdapter positionAdapter;
        int indice = 2;

        public EditProfilePage(Insegnante insegnante)
        {
            InitializeComponent();
            this.insegnante = insegnante;
            this.positionAdapter = new PositionAdapter();
            this.StL_materie.Children.Add(this.en1_materie);
        }

        private async void bt_SalvaProfilo_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            insegnante.nomeDaVisualizzare = this.en_nome.Text;
            insegnante.tariffa = float.Parse(this.en_tariffa.Text);
            if (this.cb_gruppo.IsChecked)
                insegnante.gruppo = 1;
            else
                insegnante.gruppo = 0;
            insegnante.posizione = await positionAdapter.Indirizzo2Posizione(en_indirizzo.Text);
            this.IsEnabled = true;
            Contatti c = new Contatti();
            c.cellulare = this.en_cellulare.Text;
            c.emailContatto = this.en_email.Text;
            insegnante.contatti = c;
            List<Materia> materie = new List<Materia>(); //va sostituito con la lista di materie dell'insegnante
            foreach(var element in this.StL_materie.Children)
            {
                Entry entry = (Entry)element;
                materie.Add(new Materia(entry.Text)); //va sostituito con la lista di materie dell'insegnante
            }
            foreach (Materia m in materie)
                Console.WriteLine(m);
            //insegnante.dataOraRegistrazione = DateTime.Now;
            insegnante.id = int.Parse(Preferences.Get("id", (-1).ToString()));
            if (insegnante.id != -1)
            {
                InsegnantiService.Save(insegnante);
                Preferences.Set("isInsegnante", true);
            }
            else
                Console.WriteLine("Errore");
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void img_profileImage_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private void btn_addMateria_Clicked(object sender, EventArgs e)
        {
            var entry = new Entry { Placeholder= "Materia " + indice};
            AutomationProperties.SetIsInAccessibleTree(entry, true);
            AutomationProperties.SetName(entry, "entry" + indice.ToString());
            indice++;
            this.StL_materie.Children.Add(entry);
        }
    }
}