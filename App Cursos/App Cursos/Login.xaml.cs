using App_Cursos.Data;
using App_Cursos.Models;
using Plugin.LocalNotification;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            this.NotificationNextCourses();
        }
        private async void Button_Login(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailLog.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir un Email en el Campo", "✅OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraLog.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir la Contraseña", "✅OK");
                return;
            }
            var resultado = await App.SQLiteDB.GetUsersValidate(txtEmailLog.Text, txtContraLog.Text);

            if (resultado.Count > 0)
            {
                txtEmailLog.Text = "";
                txtContraLog.Text = "";

                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("❌AVISO", "El Email o la Contaseña esta Incorretco", "✅OK");
                txtEmailLog.Text = "";
                txtContraLog.Text = "";
                await Navigation.PushAsync(new Registro());
            }
        }

        private async void Button_Registrarse(object sender, EventArgs e)
        {
            txtEmailLog.Text = "";
            txtContraLog.Text = "";

            await Navigation.PushAsync(new Registro());
        }

        #region Methods

        private async Task NotificationNextCourses()
        {
            try
            {
                
                var cursos = await App.SQLiteDB.GetCourseByDate();

                // Checando cursos del día
                List<Seguimiento> _cursos = ((List<Seguimiento>)cursos).Where(c=>c.FechaCurso.Year == DateTime.Now.Year 
                                                                                    && c.FechaCurso.Month == DateTime.Now.Month
                                                                                    && c.FechaCurso.Day == DateTime.Now.Day).ToList();

                if (_cursos.Count > 0)
                {
                    var request = new NotificationRequest
                    {
                        NotificationId = 100,
                        Title = "App Cursos",
                        Description = "Tienes cursos programados el día de hoy"
                    };


                    await NotificationCenter.Current.Show(request);
                }


                // Checando cursos de un día antes
                List<Seguimiento> _cursos01 = ((List<Seguimiento>)cursos).Where(c => c.FechaCurso.Year == DateTime.Now.Year
                                                                                    && c.FechaCurso.Month == DateTime.Now.Month
                                                                                    && c.FechaCurso.Day == (DateTime.Now.Day - 1)).ToList();

                if (_cursos01.Count > 0)
                {
                    var request = new NotificationRequest
                    {
                        NotificationId = 100,
                        Title = "App Cursos",
                        Description = "Tienes cursos programados para el día de mañana"
                    };


                    await NotificationCenter.Current.Show(request);
                }


            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        #endregion
    }
}