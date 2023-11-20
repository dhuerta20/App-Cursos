using App_Cursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Cursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }
        public async void btn_Registrar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailReg.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir un Email en el Campo", "✅OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraReg.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir una Contraseña en el Campo", "✅OK");
                return;
            }
            if (string.IsNullOrEmpty(txtNombreReg.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir un Nombre en el Campo", "✅OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEdadReg.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir una Edad en el Campo", "✅OK");
                return;
            }
            Users usr = new Users()
            {
                EmailUser = txtEmailReg.Text,
                EmailPassword = txtContraReg.Text,
                NombreCompleto = txtNombreReg.Text,
                Edad = int.Parse(txtEdadReg.Text),
                FechaCreacion = DateTime.Now
            };
            await App.SQLiteDB.SaveUserModelAsync(usr);

            await DisplayAlert("❌AVISO", "Registro Guardado", "✅OK");
            txtEmailReg.Text = "";
            txtContraReg.Text = "";
            txtNombreReg.Text = "";
            txtEdadReg.Text = "";

            await Navigation.PopAsync();
        }
    }
}