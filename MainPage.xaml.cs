using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppCursos.Models;
using System.IO;
using SQLite;


namespace AppCursos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Empleado emple = new Empleado()
                {
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoP.Text,
                    ApellidoMaterno = txtApellidoM.Text,
                    Direccion = txtDireccion.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTelefono.Text),
                    Curp = txtCurp.Text,
                    TipoEmpleado = pickerTipoEmp.SelectedItem.ToString()
                    
                };

                await App.SQLiteDB.SaveEmpleadosAsync(emple);

                txtNombre.Text = "";
                txtApellidoP.Text = "";
                txtApellidoM.Text = "";
                txtDireccion.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtCurp.Text = "";
                pickerTipoEmp.SelectedIndex = -1;

                await DisplayAlert("AVISO", "Guardado de forma exitosa", "Ok");
                
                var EmpleadoList = await App.SQLiteDB.GetEmpleadosAsync();

                if (EmpleadoList != null)
                {
                    lsEmpleados.ItemsSource = EmpleadoList;
                }


            }
            else
            {
                await DisplayAlert("AVISO", "Ingresar todos los datos", "Ok");

            }
        }

        public async void llenarDatos()
        {
            var EmpleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (EmpleadoList != null) 
            {
                lsEmpleados.ItemsSource = EmpleadoList;
            }
            
        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoP.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoM.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCurp.Text))
            {
                respuesta = false;
            }
            else if ((pickerTipoEmp.SelectedIndex == -1))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                    Empleado empleado = new Empleado()
                {
                    IdEmp = int.Parse(txtIdEmp.Text),
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoP.Text,
                    ApellidoMaterno = txtApellidoM.Text,
                    Direccion = txtDireccion.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTelefono.Text),
                    Curp = txtCurp.Text,
                    TipoEmpleado = pickerTipoEmp.SelectedItem.ToString()
                   
                };

                await App.SQLiteDB.SaveEmpleadosAsync(empleado);
                txtIdEmp.Text = "";
                txtNombre.Text = "";
                txtApellidoP.Text = "";
                txtApellidoM.Text = "";
                txtDireccion.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtCurp.Text = "";
                pickerTipoEmp.SelectedIndex = -1;

                txtIdEmp.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("AVISO", "Se actualizo registro de manera exitosa", "OK");
                llenarDatos();
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));

            if (empleado != null )
            {
                await App.SQLiteDB.DeleteEmpleadoAsync(empleado);
                await DisplayAlert("AVISO", "Se elimino el registro de manera exitosa", "OK");

                txtIdEmp.Text = "";
                txtNombre.Text = "";
                txtApellidoP.Text = "";
                txtApellidoM.Text = "";
                txtDireccion.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtCurp.Text = "";
                pickerTipoEmp.SelectedIndex = -1;

                txtIdEmp.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos();

            }
        }

        private async void lstEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleado)e.SelectedItem;

            btnGuardar.IsVisible = false;
            txtIdEmp.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if(!string.IsNullOrEmpty(obj.IdEmp.ToString()))
            {
                var emplea = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.IdEmp);

                if (emplea != null)
                {
                    txtIdEmp.Text = emplea.IdEmp.ToString();
                    txtNombre.Text = emplea.Nombre;
                    txtApellidoP.Text = emplea.ApellidoPaterno;
                    txtApellidoM.Text = emplea.ApellidoMaterno;
                    txtDireccion.Text = emplea.Direccion;
                    txtEdad.Text = emplea.Edad.ToString();
                    txtTelefono.Text = emplea.Telefono.ToString();
                    txtCurp.Text = emplea.Curp.ToString();
                    pickerTipoEmp.SelectedItem = emplea.TipoEmpleado.ToString();

                }
            }

        }
    }
}
