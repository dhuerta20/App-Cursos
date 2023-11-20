using App_Cursos.Models;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace App_Cursos
{
    public partial class MainPage : ContentPage
    {

        // Private variable
        string fotoEmpleado = "";

        public MainPage()
        {
            InitializeComponent();
            
            txtTipo_de_Empleado.Items.Add("Planta");
            txtTipo_de_Empleado.Items.Add("Temporal");
            llenarDatos();

            NotificationCenter.Current.NotificationReceived += ShowCustomAlertFromNotification;

        }

        private async void Registrar_Button_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Empleados emple = new Empleados
                {
                    Nombre_de_Empleado = txtNombre_de_Empleado.Text,
                    Dirección = txtDirección.Text,
                    Teléfono = double.Parse(txtTeléfono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    Tipo_de_Empleado = txtTipo_de_Empleado.Items[txtTipo_de_Empleado.SelectedIndex],
                    Foto = fotoEmpleado
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emple);

                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedIndex = -1;
                await DisplayAlert("❌AVISO", "Se Registro Empleado", "✅Ok");
                llenarDatos();

                var EmpleadosList = await App.SQLiteDB.GetEmpleadosAsync();
                if (EmpleadosList != null)
                {
                    lsEmpleados.ItemsSource = EmpleadosList;
                }

            }
            else
            {
                await DisplayAlert("❌AVISO", "Ingresar los Datos", "✅Ok");
            }
        }
        private async void Button_Actualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                Empleados empleado = new Empleados()
                {
                    IDEmp = int.Parse(txtIdEmp.Text),
                    Nombre_de_Empleado = txtNombre_de_Empleado.Text,
                    Dirección = txtDirección.Text,
                    Teléfono = double.Parse(txtTeléfono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    Tipo_de_Empleado = txtTipo_de_Empleado.Items[txtTipo_de_Empleado.SelectedIndex],
                    Foto = fotoEmpleado
                };

                await App.SQLiteDB.SaveEmpleadoAsync(empleado);
                txtIdEmp.Text = "";
                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedIndex = -1;
                this.imgPhoto.Source = "default_image.png";

                txtIdEmp.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("❌AVISO", "Se Actualizo Registro de Manera Exitosa", "✅OK");
                llenarDatos();
            }
        }
        public async void Borrar_Button_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));
            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadosAsync(empleado);
                await DisplayAlert("❌AVISO", "Se Elimino el Registro de Manera Exitosa", "✅OK");
                txtIdEmp.Text = "";
                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedIndex = -1;

                txtIdEmp.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos();
            }
        }
        public async void llenarDatos()
        {
            List<Empleados> lstEmpleados = new List<Empleados>();

            var EmpleadoList = await App.SQLiteDB.GetEmpleadosAsync();

            if (EmpleadoList != null)
            {

                foreach(var i in EmpleadoList)
                {
                    Empleados _empleados = new Empleados();

                    _empleados.Foto = i.Foto;
                    _empleados.IDEmp = i.IDEmp;
                    _empleados.Nombre_de_Empleado = i.Nombre_de_Empleado;
                    _empleados.Dirección = i.Dirección;
                    _empleados.Teléfono = i.Teléfono;
                    _empleados.Edad = i.Edad;
                    _empleados.CURP = i.CURP;
                    _empleados.Tipo_de_Empleado = i.Tipo_de_Empleado;

                    lstEmpleados.Add(_empleados);

                }

                lsEmpleados.ItemsSource = lstEmpleados;
            }
        }

        private async void lstEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;

            btnRegistrar.IsVisible = false;
            txtIdEmp.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            try
            {
                if (!string.IsNullOrEmpty(obj.IDEmp.ToString()))
                {
                    var emplea = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.IDEmp);

                    if (emplea != null)
                    {
                        imgPhoto.Source = emplea.Foto;
                        txtIdEmp.Text = emplea.IDEmp.ToString();
                        txtNombre_de_Empleado.Text = emplea.Nombre_de_Empleado;
                        txtDirección.Text = emplea.Dirección;
                        txtTeléfono.Text = emplea.Teléfono.ToString();
                        txtEdad.Text = emplea.Edad.ToString();
                        txtCURP.Text = emplea.CURP.ToString();
                        txtTipo_de_Empleado.SelectedIndex = emplea.Tipo_de_Empleado == "Planta" ? 0 : 1;

                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }


        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre_de_Empleado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDirección.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTeléfono.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCURP.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTipo_de_Empleado.Title))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnCapturePhoto_Clicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            

            if (photo != null)
            {
                var photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(photoPath))
                    await stream.CopyToAsync(newStream);


                this.imgPhoto.Source = photoPath;

                fotoEmpleado = photoPath;
            }

            var photo2 = await MediaPicker.PickPhotoAsync();

            if (photo2 != null)
            {
                var photoPath = Path.Combine(FileSystem.CacheDirectory, photo2.FileName);
                using (var stream = await photo2.OpenReadAsync())
                using (var newStream = File.OpenWrite(photoPath))
                    await stream.CopyToAsync(newStream);


                this.imgPhoto.Source = photoPath;

                fotoEmpleado = photoPath;
            }
        }


        #region Notifications

        private void ShowCustomAlertFromNotification(NotificationEventArgs e)
        {

        }



        #endregion
    }
}