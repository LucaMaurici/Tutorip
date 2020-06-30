using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Crediti : ContentPage
    {
        public Crediti()
        {
            InitializeComponent();
        }

        private void bt_indietro_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}