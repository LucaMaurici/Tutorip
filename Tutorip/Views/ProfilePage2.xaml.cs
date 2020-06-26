using System;
using System.Collections.Generic;
using Tutorip.Models;
using Tutorip.Services;
using Tutorip.Services.ModelService;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage2 : ContentPage
    {

        Insegnante insegnante;

        public ProfilePage2(Insegnante insegnante)
        {
            InitializeComponent();
            this.btn_salvaRecensione.IsVisible = false;
            this.insegnante = insegnante;
            gestisciVisibilita();
            this.completeProfile();
            this.popolaRecensioni();
        }

        private void gestisciVisibilita()
        {
            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id)
            {
                this.stl_preferiti.IsVisible = false;
                this.fr_lasciaRecensione.IsVisible = false;
            }
            else if(Preferences.Get("id",null) != null)
            {
                this.stl_preferiti.IsVisible = true;
                this.fr_lasciaRecensione.IsVisible = true;
                this.stl_visibilità.IsVisible = false;
                this.stl_modifica.IsVisible = false;
            }
            else
            {
                this.stl_preferiti.IsVisible = false;
                this.fr_lasciaRecensione.IsVisible = false;
                this.stl_visibilità.IsVisible = false;
                this.stl_modifica.IsVisible = false;
            }
        }

        /*                  <Image Source="star1" WidthRequest="28" HeightRequest="28" TranslationY="1"></Image>
                            <Label x:Name="eval_lbl" FontAttributes="Bold" HorizontalOptions="StartAndExpand">
                                <Label.FormattedText>
                                    <FormattedString>
                                        <Span FontSize="40" FontAttributes="Bold" Text="{Binding insegnante.valutazioneMedia}"></Span>
                                        <Span FontSize="30" FontAttributes="Bold"></Span>
                                    </FormattedString>
                                </Label.FormattedText>
                            </Label>*/

        private void popolaRecensioni()
        {
            if (insegnante.recensioni != null)
            {
                foreach (Recensione recensione in this.insegnante.recensioni)
                {
                    var frame = new Frame { Padding = new Thickness(30,10,30,20) };
                    this.stl_recensioni.Children.Add(frame);
                    var stack = new StackLayout();
                    frame.Content = stack;
                    var stackHor = new StackLayout { Orientation = StackOrientation.Horizontal };
                    stack.Children.Add(stackHor);
                    stackHor.Children.Add(new Label { Text = recensione.titolo, FontSize = 25, FontAttributes=FontAttributes.Bold, HorizontalOptions = LayoutOptions.StartAndExpand });
                    var stackVal = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0,0,40,0) };
                    stackHor.Children.Add(stackVal);
                    stackVal.Children.Add(new Image { Source = "star1", WidthRequest = 28, HeightRequest = 28, TranslationY = 1 });
                    stackVal.Children.Add(new Label { Text = recensione.valutazioneGenerale.ToString(), FontSize = 30, FontAttributes = FontAttributes.Bold }); //chiamata a formattatore di stringa valutazione
                    stack.Children.Add(new Label { Text = recensione.corpo, Margin = 5 });
                    if (recensione.anonimo == 0)
                        stack.Children.Add(new Label
                        {
                            Text = recensione.utente.nome + " " + recensione.utente.cognome,
                            FontSize = 15,
                            FontAttributes = FontAttributes.Italic,
                            HorizontalTextAlignment = TextAlignment.End,
                            Margin = new Thickness(0,0,20,0)
                        });
                }
            }
        }

        private void completeProfile()
        {
            if (insegnante.gruppo == 1)
                this.lb_gruppo.Text = "Lezione di gruppo su richiesta";
            else
                this.lb_gruppo.Text = "Lezione in singolo";

            if (insegnante.modalita == 0)
                this.lb_mod.Text = "In presenza";
            if (insegnante.modalita == 1)
                this.lb_mod.Text = "A distanza";
            if (insegnante.modalita == 2)
                this.lb_mod.Text = "In presenza e a distanza";

            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id)
                this.distanza_lbl.Text = insegnante.posizione.indirizzo;

            this.lb_email.Text = insegnante.contatti.emailContatto;
            this.eval_lbl.Text = insegnante.valutazioneMedia;
            this.name_lbl.Text = insegnante.nomeDaVisualizzare;
            this.tariffa_spn.Text = insegnante.tariffa.ToString();
            
            //List<string> materie = new List<Materia>();
            //foreach (Materia m in insegnante.materie)
            //    materie.Add(m);
            //this.subject_list.ItemsSource = insegnante.materie;

            foreach(Materia materia in this.insegnante.materie)
            {
                this.flx_materie.Children.Add(new Button { 
                    Text = materia.nome, HorizontalOptions=LayoutOptions.Start, 
                    BackgroundColor=Color.Transparent, FontSize=13, TextColor=Color.FromHex("#0E5D90"), BorderColor=Color.FromHex("#0E5D90"),
                    BorderWidth=1, CornerRadius=20, Padding=10, Margin=3
                });
            }
        }

        private void bt_indietro_Clicked(object sender, System.EventArgs e)
        {
            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id)
            {
                Navigation.InsertPageBefore(new MenuPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (int.Parse(Preferences.Get("id", (0).ToString())) == insegnante.id) {
            Navigation.InsertPageBefore(new MenuPage(), this);
            Navigation.PopAsync();
            }   
            else
            {
                Navigation.PopAsync();
            }
            return true;
        }

        private async void bt_ModificaProfilo_Clicked(object sender, System.EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PushAsync(new EditProfilePage(this.insegnante)); // conveniente pushare???
            this.IsEnabled = true;
        }

        private void bt_ModificaVisibilità_Clicked(object sender, System.EventArgs e)
        {

        }

        private void bt_addFav_Clicked(object sender, EventArgs e) // bottone per aggiungere ai preferiti da mostrare solo se si è utenti che vedono il profilo
        {
            InsegnantiService.aggiungiPreferito(int.Parse(Preferences.Get("id", null)), insegnante.id);
        }

        private void btn_recensione_Clicked(object sender, EventArgs e)
        {

            var entryTitolo = new Entry { Placeholder = "Titolo", Keyboard = Keyboard.Text };
            var entryCorpo =  new Entry { Placeholder = "Testo della recensione", Keyboard = Keyboard.Text };
            var entryValMed =  new Entry { Placeholder = "ValutazioneMedia", Keyboard = Keyboard.Numeric };
            var entryEmpatia =  new Entry { Placeholder = "Empatia", Keyboard = Keyboard.Numeric };
            var entrySpieg = new Entry { Placeholder = "Spiegazione", Keyboard = Keyboard.Numeric };
            var entryOrg = new Entry { Placeholder = "Organizzazione", Keyboard = Keyboard.Numeric };
            
            this.stl_recensione.Children.Add(entryTitolo);
            this.stl_recensione.Children.Add(entryCorpo);
            this.stl_recensione.Children.Add(entryValMed);
            this.stl_recensione.Children.Add(entryEmpatia);
            this.stl_recensione.Children.Add(entrySpieg);
            this.stl_recensione.Children.Add(entryOrg);

            this.btn_salvaRecensione.IsVisible = true;
        }

        private void btn_salvaRecensione_Clicked(object sender, EventArgs e)
        {
            Recensione r = new Recensione();

            r.cod_insegnante = insegnante.id;
            r.cod_utente = int.Parse(Preferences.Get("id", (-1).ToString()));

            if (this.cb_isAnonimo.IsChecked)
                r.anonimo = 1;
            else
                r.anonimo = 0;

            Entry entryTitolo = (Entry) stl_recensione.Children[0];
            r.titolo = entryTitolo.Text;
            Entry entryCorpo = (Entry)stl_recensione.Children[1];
            r.corpo = entryCorpo.Text;
            Entry entryValMed = (Entry)stl_recensione.Children[2];
            r.valutazioneGenerale = int.Parse(entryValMed.Text);
            Entry entryEmp = (Entry)stl_recensione.Children[3];
            r.empatia = int.Parse(entryEmp.Text);
            Entry entrySpieg = (Entry)stl_recensione.Children[4];
            r.spiegazione = int.Parse(entrySpieg.Text);
            Entry entryOrg = (Entry)stl_recensione.Children[5];
            r.organizzazione = int.Parse(entryOrg.Text);

            RecensioniService.Save(r);
        }
    }
}