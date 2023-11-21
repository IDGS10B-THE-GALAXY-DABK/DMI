using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCursos.Models;

namespace AppCursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        // Validar Correo y Contraseña de Usuario
        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailLog.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir el email", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(txtContraLog.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir la contraseña", "Ok");
                return;
            }

            var resultado = await App.SQLiteDB.GetUsersValidate(txtEmailLog.Text, txtContraLog.Text);

            if (resultado.Count > 0)
            {
                txtEmailLog.Text = "";
                txtContraLog.Text = "";

                await Navigation.PushAsync(new Menu());
            }
            else
            {
                txtEmailLog.Text = "";
                txtContraLog.Text = "";
                await DisplayAlert("AVISO", "El email o la contraseña estan incorrectos", "Ok");
                return;
            }

        }


        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            txtEmailLog.Text = "";
            txtContraLog.Text = "";

            await Navigation.PushAsync(new Registro());
        }
    }
}