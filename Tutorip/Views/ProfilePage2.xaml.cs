using Tutorip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutorip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage2 : ContentPage
    {
        public ProfilePage2(Insegnante item)
        {
            InitializeComponent();
            name_lbl.Text = item.email;

        }
    }
}