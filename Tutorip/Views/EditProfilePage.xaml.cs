using System;
using System.Collections.Generic;
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
        int indiceCampoMateria = 2;

        public EditProfilePage(Insegnante insegnante)
        {
            InitializeComponent();
            this.insegnante = insegnante;
            this.positionAdapter = new PositionAdapter();
            completaCampi();
            this.StL_materie.Children.Add(this.en1_materie);
        }

        private void completaCampi()
        {
            this.en_nome.Text = insegnante.nomeDaVisualizzare;
            this.en_tariffa.Text = insegnante.tariffa.ToString();
            this.en_indirizzo.Text = insegnante.posizione.indirizzo;
            this.en_cellulare.Text = insegnante.contatti.cellulare;
            this.en_email.Text = insegnante.contatti.emailContatto;
            //this.en_facebook.Text = insegnante.contatti.facebook;
            if (insegnante.gruppo == 1)
                this.cb_gruppo.IsChecked = true;
            else
                this.cb_gruppo.IsChecked = false;
        }

        private async void bt_SalvaProfilo_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            //nome
            insegnante.nomeDaVisualizzare = this.en_nome.Text;
            //tariffa
            insegnante.tariffa = float.Parse(this.en_tariffa.Text);
            //gruppo
            if (this.cb_gruppo.IsChecked)
                insegnante.gruppo = 1;
            else
                insegnante.gruppo = 0;

            //posizione
            if (en_indirizzo.Text != null) {
                Posizione pos = new Posizione();
                pos = await positionAdapter.Indirizzo2Posizione(en_indirizzo.Text);
                pos.id = insegnante.posizione.id;
                insegnante.posizione = pos;
            }
            //contatti
            Contatti c = new Contatti();
            c.cellulare = this.en_cellulare.Text;
            c.emailContatto = this.en_email.Text;
            c.facebook = this.en_facebook.Text;
            insegnante.contatti = c;

            //materie
            if(!Preferences.Get("isInsegnante", false))
                insegnante.materie = new List<Materia>();

            Entry en = (Entry) StL_materie.Children[0];
            if (en.Text != null) 
            {
                foreach (var element in this.StL_materie.Children)
                {
                    Entry entry = (Entry)element;
                    insegnante.materie.Add(new Materia(entry.Text));
                }
            }
            
            //descrizione
            insegnante.descrizione = new List<SezioneProfilo>();
            int indice = 1;
            foreach(var element in this.stl_descrizione.Children)
            {
                Frame frame = (Frame)element;
                StackLayout stack = (StackLayout) frame.Content;
                SezioneProfilo s = new SezioneProfilo();
                //Entry entryIndice = (Entry)stack.Children[0];
                s.indice = indice;//int.Parse(entryIndice.Text);
                Entry entryTitolo = (Entry)stack.Children[0];
                s.titolo = entryTitolo.Text;
                Entry entryCorpo = (Entry)stack.Children[1];
                s.corpo = entryCorpo.Text;
                s.idInsegnante = insegnante.id;
                this.insegnante.descrizione.Add(s);
                indice++;
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
                Navigation.InsertPageBefore(new ProfilePage2(insegnante, "salva"), this);
                await Navigation.PopAsync();
                //await Navigation.PushAsync(new ProfilePage2(insegnante));
            }
            else
                Console.WriteLine("Errore");
            this.IsEnabled = true;
        }

        private void bt_indietro_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("isInsegnante", false))
            {
                //Navigation.InsertPageBefore(new ProfilePage2(insegnante, "indietro"), this);
                Navigation.PopAsync();
            }
            else
            {
                Navigation.InsertPageBefore(new MenuPage(), this);
                Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if(Preferences.Get("isInsegnante", false))
            {
                //Navigation.InsertPageBefore(new ProfilePage2(insegnante, "indietro"), this);
                Navigation.PopAsync();

            }
            else
            {
                Navigation.InsertPageBefore(new MenuPage(), this);
                Navigation.PopAsync();
            }
            return true;
        }

        private void img_profileImage_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            this.IsEnabled = true;
        }

        private void btn_addMateria_Clicked(object sender, EventArgs e)
        {
            var entry = new Entry { Placeholder= "Materia " + indiceCampoMateria, TextColor = Color.FromHex("#666"), Margin =5 };
            //AutomationProperties.SetIsInAccessibleTree(entry, true);
            //AutomationProperties.SetName(entry, "entry" + indice.ToString());
            entry.Keyboard = Keyboard.Create(KeyboardFlags.Suggestions);
            indiceCampoMateria++;
            this.StL_materie.Children.Add(entry);
            this.scrollView.FadeTo(1, 1500, Easing.CubicIn);
            this.scrollView.ScrollToAsync(0, entry.Y+100, true);
        }
        //<Entry x:Name="en_nome" Placeholder="Nome C." WidthRequest="200" Keyboard="Text" HorizontalTextAlignment="Center" TextColor="#666" FontAttributes="Bold" FontSize="25" Margin="5,0,5,0" TranslationX="-3" HorizontalOptions="CenterAndExpand"></Entry>
        private void btn_addSezione_Clicked(object sender, EventArgs e)
        {
            var frame = new Frame { Padding = new Thickness(30,20,30,20) };
            var Stack = new StackLayout { Orientation = StackOrientation.Vertical };
            //var entryIndice = new Entry { Placeholder = "Indice", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.End};
            var entryTitolo = new Entry { Placeholder="Titolo", Keyboard=Keyboard.Text, TextColor=Color.FromHex("#666"), FontAttributes=FontAttributes.Bold, FontSize=25, HorizontalOptions=LayoutOptions.FillAndExpand };
            var entryCorpo = new Editor { Placeholder="Corpo", Keyboard=Keyboard.Text, TextColor = Color.FromHex("#666"), HeightRequest=200, HorizontalOptions=LayoutOptions.FillAndExpand };
            //Stack.Children.Add(entryIndice);
            Stack.Children.Add(entryTitolo);
            Stack.Children.Add(entryCorpo);
            frame.Content = Stack;
            this.stl_descrizione.Children.Add(frame);
            this.scrollView.ScrollToAsync(0, frame.Y+2000, true);
        }
    }
}