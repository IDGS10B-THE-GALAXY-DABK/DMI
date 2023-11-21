using AppCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SQLite;

namespace AppCursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailReg.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir un Email en el campo", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraReg.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir una contraseña en el campo", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtNombreReg.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir el Nombre Completo en el campo", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEdadReg.Text))
            {
                await DisplayAlert("AVISO", "Debe escribir la Edad en el campo", "OK");
                return;
            }

            Users usr = new Users()
            {
                Email = txtEmailReg.Text,
                Emailpassword = txtContraReg.Text,
                NombreCompleto = txtNombreReg.Text,
                Edad = int.Parse(txtEdadReg.Text),
                FechaCreacion = DateTime.Now
            };

            await App.SQLiteDB.SaveUserModelAsync(usr);

            await DisplayAlert("AVISO", "El registro de usuario se ha guardado con exito", "OK");
            txtEmailReg.Text = "";
            txtContraReg.Text = "";
            txtNombreReg.Text = "";
            txtEdadReg.Text = "";

            await Navigation.PopAsync();
        }

    }
}