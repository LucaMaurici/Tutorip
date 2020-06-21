using Tutorip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(Insegnante item)
        {
            InitializeComponent();
            name_lbl.Text = item.nomeDaVisualizzare;

        }
    }
}