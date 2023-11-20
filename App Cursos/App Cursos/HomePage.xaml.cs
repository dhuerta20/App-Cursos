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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void Registrar_Empleado_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void Registrar_Cursos_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cursos());
        }

        private async void btnSeguimiento_Curso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeguimientoCursos());
        }
    }
}