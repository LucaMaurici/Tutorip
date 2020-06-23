using Java.Security;
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
            //nome
            Console.WriteLine(this.en_nome.Text);
            insegnante.nomeDaVisualizzare = this.en_nome.Text;
            Console.WriteLine(insegnante.nomeDaVisualizzare);
            //tariffa
            Console.WriteLine(this.en_tariffa.Text);
            insegnante.tariffa = float.Parse(this.en_tariffa.Text);
            Console.WriteLine(insegnante.tariffa);
            //gruppo
            if (this.cb_gruppo.IsChecked)
                insegnante.gruppo = 1;
            else
                insegnante.gruppo = 0;

            //posizione
            Console.WriteLine(en_indirizzo.Text);
            if (en_indirizzo.Text !=null)
                insegnante.posizione = await positionAdapter.Indirizzo2Posizione(en_indirizzo.Text);
            Console.WriteLine(insegnante.posizione);
            //contatti
            Contatti c = new Contatti();
            c.cellulare = this.en_cellulare.Text;
            c.emailContatto = this.en_email.Text;
            insegnante.contatti = c;

            //materie
            insegnante.materie = new List<Materia>();
            foreach(var element in this.StL_materie.Children)
            {
                Entry entry = (Entry)element;
                insegnante.materie.Add(new Materia(entry.Text));
            }

            //modalità
            insegnante.modalita = this.pck_modalità.SelectedIndex;

            //profiloPublico
            insegnante.profiloPubblico = 0;
            //id
            insegnante.id = int.Parse(Preferences.Get("id", (-1).ToString()));
            
            if (insegnante.id != -1)
            {
                InsegnantiService.Save(insegnante);
                Preferences.Set("isInsegnante", true);
                Navigation.InsertPageBefore(new MenuPage(), this);
                await Navigation.PopAsync();
                await Navigation.PushAsync(new ProfilePage2(insegnante));
            }
            else
                Console.WriteLine("Errore");
            this.IsEnabled = true;
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            return true;
        }

        private void img_profileImage_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private void btn_addMateria_Clicked(object sender, EventArgs e)
        {
            var entry = new Entry { Placeholder= "Materia " + indice};
            //AutomationProperties.SetIsInAccessibleTree(entry, true);
            //AutomationProperties.SetName(entry, "entry" + indice.ToString());
            entry.Keyboard = Keyboard.Create(KeyboardFlags.Suggestions);
            indice++;
            this.StL_materie.Children.Add(entry);
        }
    }
}