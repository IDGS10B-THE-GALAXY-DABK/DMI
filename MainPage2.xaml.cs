using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCursos.Models;
using System.IO;
using SQLite;


namespace AppCursos
{
    public partial class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            InitializeComponent();
            llenarDatosCurs();
        }


        private async void btnGuardarCurso_Clicked(object sender, EventArgs e)
        {
            if (validarDatosCurs())
            {
                Curso curs = new Curso()
                {
                    NombreCurso = txtNombreCurso.Text,
                    TipoCurso = pickerTipoCurso.SelectedItem.ToString(),
                    Descripcion = txtDescripCurs.Text,
                    Tiempo = txtTiempoCurs.Text

                };

                await App.SQLiteDB.SaveCursosAsync(curs);

                txtIdCurso.Text = "";
                txtNombreCurso.Text = "";
                pickerTipoCurso.SelectedIndex = -1;
                txtDescripCurs.Text = "";
                txtTiempoCurs.Text = "";

                await DisplayAlert("AVISO", "Guardado de forma exitosa", "OK");
                var CursoList = await App.SQLiteDB.GetCursosAsync();

                if (CursoList != null)
                {
                    lsCursos.ItemsSource = CursoList;
                }

            }
            else
            {
                await DisplayAlert("AVISO", "Ingresar todos los datos correspondientes", "OK");
            }
        }

        public async void llenarDatosCurs()
        {
            var CursoList = await App.SQLiteDB.GetCursosAsync();
            if (CursoList != null)
            {
                lsCursos.ItemsSource = CursoList;
            }
        }

        public bool validarDatosCurs()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombreCurso.Text))
            {
                respuesta = false;
            }
            else if ((pickerTipoCurso.SelectedIndex == -1))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripCurs.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTiempoCurs.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }


        private async void btnActualizarCurso_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtIdCurso.Text))
            {
                Curso curso = new Curso()
                {
                    IdCurso = int.Parse(txtIdCurso.Text),
                    NombreCurso = txtNombreCurso.Text,
                    TipoCurso = pickerTipoCurso.SelectedItem.ToString(),
                    Descripcion = txtDescripCurs.Text,
                    Tiempo = txtTiempoCurs.Text
                };

                await App.SQLiteDB.SaveCursosAsync(curso);

                await DisplayAlert("AVISO", "Se actualizo el curso de manera exitosa", "OK");
                txtIdCurso.Text = "";
                txtNombreCurso.Text = "";
                pickerTipoCurso.SelectedIndex =-1;
                txtDescripCurs.Text = "";
                txtTiempoCurs.Text = "";

                txtIdCurso.IsVisible = false;
                btnGuardarCurso.IsVisible = true;
                btnActualizarCurso.IsVisible = false;
                llenarDatosCurs();

            }
        }

        private async void btnEliminarCurso_Clicked(object sender, EventArgs e)
        {
            var curso = await App.SQLiteDB.GetCursoByIdAsync(int.Parse(txtIdCurso.Text));

            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursoAsync(curso);
                await DisplayAlert("AVISO", "Se elimino el registro de manera exitosa", "OK");

                txtIdCurso.Text = "";
                txtNombreCurso.Text = "";
                pickerTipoCurso.SelectedIndex = -1;
                txtDescripCurs.Text = "";
                txtTiempoCurs.Text = "";

                txtIdCurso.IsVisible = false;
                btnGuardarCurso .IsVisible = true;
                btnActualizarCurso .IsVisible = false;
                btnEliminarCurso .IsVisible = false;
                llenarDatosCurs();
            }
        }

        private async void lsCursos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Curso)e.SelectedItem;

            btnGuardarCurso.IsVisible = false;
            txtIdCurso.IsVisible = true;
            btnActualizarCurso.IsVisible = true;
            btnEliminarCurso.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdCurso.ToString()))
            {
                var curs = await App.SQLiteDB.GetCursoByIdAsync(obj.IdCurso);

                if(curs != null)
                {
                    txtIdCurso.Text = curs.IdCurso.ToString();
                    txtNombreCurso.Text = curs.NombreCurso;
                    pickerTipoCurso.SelectedItem = curs.TipoCurso;
                    txtDescripCurs.Text = curs.Descripcion;
                    txtTiempoCurs.Text = curs.Tiempo;

                }
            }
        }
    }
}