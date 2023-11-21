using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private async void btnEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void btnCursos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage2());
        }

        private async void SegCursps_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SegCursos());

        }
    }
}