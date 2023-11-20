using System;
using System.Collections.Generic;
using System.Text;

namespace App_Cursos.Model
{
    public class CursoEmpleado
    {

        public int CursoId {  get; set; }

        public string Curso {  get; set; }

        public string TipoCurso {  get; set; }

        public int EmpleadoId { get; set; } 

        public string NombreEmpleado { get; set; }

        public string Foto {  get; set; }

        public int TrackingId {  get; set; }

    }
}
