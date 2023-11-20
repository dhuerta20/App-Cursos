using App_Cursos.Model;
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
    public partial class SeguimientoCursos : ContentPage
    {

        #region Methods

        private async void guardar()
        {
            try
            {
                List<CursoEmpleado> EmployeeCourse = new List<CursoEmpleado>();
                Seguimiento cursos = new Seguimiento();


                cursos.Lugar_del_Curso = this.txtLugarCurso.Text;
                cursos.IDEmp = ((Empleados)this.txtNombreEmp.SelectedItem).IDEmp;
                cursos.IDCso = ((CursosE)txtNombreCso.SelectedItem).IDCso;
                cursos.FechaCurso = this.txtFecha.Date;
                cursos.HoraCurso = this.txtHora.Time;
                cursos.Estatus = this.txtEstatus.SelectedItem.ToString();
                cursos.Calificacion = Convert.ToByte(this.entCalificacion.Text);
                cursos.Reminder = this.sRecordatorio.IsToggled;


                if (await App.SQLiteDB.SaveCourseTracking(cursos) == 1)
                {
                    await DisplayAlert("❌AVISO", "Registro Guardado", "✅OK");

                    this.txtNombreCso.SelectedIndex = -1;
                    this.txtNombreEmp.SelectedIndex = -1;
                    this.txtLugarCurso.Text = "";
                    this.txtFecha.Date = DateTime.Now;
                    this.txtEstatus.SelectedIndex = -1;
                    this.entCalificacion.Text = "";
                    this.sRecordatorio.IsToggled = false;
                }

                List<Seguimiento> lstSeguimiento = await App.SQLiteDB.GetCoursesByEmployeeId(cursos.IDEmp);
                List<CursoEmpleado> lsEmpleadoCurso = new List<CursoEmpleado>();


                foreach (var i in lstSeguimiento)
                {
                    CursoEmpleado cursoEmpleado = new CursoEmpleado();
                    CursosE cursosE = new CursosE();
                    Empleados employee = new Empleados();

                    cursosE = await App.SQLiteDB.GetCursosByIdAsync(i.IDCso);
                    employee = await App.SQLiteDB.GetEmpleadoByIdAsync(i.IDEmp);

                    cursoEmpleado.CursoId = i.IDCso;
                    cursoEmpleado.Curso = cursosE.Nombre_del_Curso;
                    cursoEmpleado.TipoCurso = cursosE.Tipo_de_Curso;
                    cursoEmpleado.EmpleadoId = i.IDEmp;
                    cursoEmpleado.NombreEmpleado = employee.Nombre_de_Empleado;
                    cursoEmpleado.Foto = employee.Foto;

                    EmployeeCourse.Add(cursoEmpleado);
                }

                this.cursosLst.ItemsSource = EmployeeCourse;

            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        #endregion

        public SeguimientoCursos()
        {
            InitializeComponent();
            txtEstatus.Items.Add("Programado");
            txtEstatus.Items.Add("En Progreso");
            txtEstatus.Items.Add("Completado");
            llenarDatos2();
            llenarDatos3();
        }

        public async void llenarDatos2()
        {
            var EmpleadoList2 = await App.SQLiteDB.GetEmpleadosAsync();
            if (EmpleadoList2 != null)
            {
                txtNombreEmp.ItemsSource = EmpleadoList2;
            }
        }

        public async void llenarDatos3()
        {
            var CursoList2 = await App.SQLiteDB.GetCursosAsync();
            if (CursoList2 != null)
            {
                txtNombreCso.ItemsSource = CursoList2;
            }
        }

        private void btnGuardarSeg_Clicked(object sender, EventArgs e)
        {
            this.guardar();
        }

        private void lsEmpleadoCurso_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}