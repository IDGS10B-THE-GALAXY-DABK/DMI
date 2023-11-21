using AppCursos.Models;
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
	public partial class SegCursos : ContentPage
	{
        private List<Empleado> empleados;

        public SegCursos ()
		{
			InitializeComponent ();
            CargarDatosEmpleado();
            CargarDatosCursos();
            llenarDatos();

        }

        private async void CargarDatosEmpleado()
        {
            empleados = await App.SQLiteDB.GetEmpleadosAsync();

            if (empleados != null && empleados.Count > 0)
            {
                // Asigna la lista de empleados al origen de datos del Picker
                txtNomEmp.ItemsSource = empleados.Select(e => e.FullName).ToList();
            }
        }

        private async void CargarDatosCursos()
        {
            var cursos = await App.SQLiteDB.GetCursosAsync();

            if (cursos != null && cursos.Count > 0)
            {
                txtNomCurso.ItemsSource = cursos.Select(e => e.NombreCurso).ToList();
            }
        }

        public async void llenarDatos()
        {
            var seguimientoList = await App.SQLiteDB.GetSeguimientoAsync();
            if (seguimientoList != null)
            {
                lsSeguimiento.ItemsSource = seguimientoList;
            }
        }

        private async void btnGuardarSeg_Clicked(object sender, EventArgs e)
        {
            {
                if (txtNomEmp.SelectedIndex == -1 || txtNomCurso.SelectedIndex == -1)
                {
                    await DisplayAlert("AVISO", "Por favor, selecciona el nombre del empleado y el nombre del curso", "OK");
                    return;
                }

                try
                {
                    int idEmpleado = empleados[txtNomEmp.SelectedIndex].IdEmp;

                    Seguimiento nuevoSeguimiento = new Seguimiento
                    {
                        IdEmpleado = idEmpleado,
                        NombreDelEmpleado = txtNomEmp.SelectedItem.ToString(),
                        NombreDelCurso = txtNomCurso.SelectedItem.ToString(),
                        LugarDelCurso = txtNomLugar.Text,
                        Fecha = txtNumFecha.Date,
                        Hora = txtNumHora.Time,
                        Estatus = pickerEstatus.SelectedItem.ToString(),
                        Calificacion = Convert.ToInt32(txtCalificacion.Text)
                    };

                    await App.SQLiteDB.SaveSeguimientoAsync(nuevoSeguimiento);

                    await DisplayAlert("AVISO", "Seguimiento guardado correctamente", "OK");

                    txtNomEmp.SelectedIndex = -1;
                    txtNomCurso.SelectedIndex = -1;
                    txtNomLugar.Text = "";
                    pickerEstatus.SelectedIndex = -1;
                    txtCalificacion.Text = "";

                    llenarDatos();

                }
                catch (Exception ex)
                {
                    await DisplayAlert("AVISO", "LLenar todos los campos correspondientes", "OK");
                }
            }
        }

        private async void btnActualizarSeg_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdSeg.Text))
            {
                Seguimiento seguimiento = new Seguimiento()
                {
                    Id = int.Parse(txtIdSeg.Text),
                    NombreDelEmpleado = txtNomEmp.SelectedItem.ToString(),
                    NombreDelCurso = txtNomCurso.SelectedItem.ToString(),
                    LugarDelCurso = txtNomLugar.Text,
                    Fecha = txtNumFecha.Date,
                    Hora = txtNumHora.Time,
                    Estatus = pickerEstatus.SelectedItem.ToString(),
                    Calificacion = int.Parse(txtCalificacion.Text),
                };

                await App.SQLiteDB.SaveSeguimientoAsync(seguimiento);

                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                txtNomEmp.SelectedIndex = -1;
                txtNomCurso.SelectedIndex = -1;
                txtNomLugar.Text = "";
                pickerEstatus.SelectedIndex = -1;
                txtCalificacion.Text = "";

                btnGuardarSeg.IsVisible = true;
                btnActualizarSeg.IsVisible = false;
                btnEliminarSeg.IsVisible = false;
                llenarDatos();
            }
        }

        private async void btnEliminarSeg_Clicked(object sender, EventArgs e)
        {
            var seguimiento = await App.SQLiteDB.GetSeguimientoByIdAsync(int.Parse(txtIdSeg.Text));

            if (seguimiento != null)
            {
                await App.SQLiteDB.DeleteSeguimientoAsync(seguimiento);
                await DisplayAlert("Seguimiento", "Se elimino de manera exitosa", "Ok");

                txtNomEmp.SelectedIndex = -1;
                txtNomCurso.SelectedIndex = -1;
                txtNomLugar.Text = "";
                pickerEstatus.SelectedIndex = -1;
                txtCalificacion.Text = "";

                llenarDatos();

                btnActualizarSeg.IsVisible = false;
                btnEliminarSeg.IsVisible = false;
                btnGuardarSeg.IsVisible = true;
            }
        }

        private async void lsSeguimiento_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Seguimiento)e.SelectedItem;

            btnGuardarSeg.IsVisible = false;
            btnActualizarSeg.IsVisible = true;
            btnEliminarSeg.IsVisible = true;


            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var seguimiento = await App.SQLiteDB.GetSeguimientoByIdAsync(obj.Id);
                if (seguimiento != null)
                {
                    txtIdSeg.Text = seguimiento.Id.ToString();
                    txtNomEmp.SelectedItem = seguimiento.NombreDelEmpleado;
                    txtNomCurso.SelectedItem = seguimiento.NombreDelCurso;
                    txtNomLugar.Text = seguimiento.LugarDelCurso;
                    txtNumFecha.Date = seguimiento.Fecha;
                    txtNumHora.Time = seguimiento.Hora;
                    pickerEstatus.SelectedItem = seguimiento.Estatus;
                    txtCalificacion.Text = seguimiento.Calificacion.ToString();
                }
            }
        }

        private void txtNomEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNomEmp.SelectedIndex != -1)
            {
                // Obtén el empleado seleccionado
                var empleadoSeleccionado = empleados[txtNomEmp.SelectedIndex];

            }
        }
    }
}