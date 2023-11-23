using Android.Media;
using App_Cursos.Model;
using App_Cursos.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App_Cursos.Data
{
    public class DataBase
    {
        SQLiteAsyncConnection db;

        public DataBase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleados>().Wait();
            db.CreateTableAsync<CursosE>().Wait();
            db.CreateTableAsync<Users>().Wait();
            db.CreateTableAsync<Seguimiento>().Wait();
        }

        public DataBase()
        {
                
        }

        public Task<int> SaveEmpleadoAsync(Empleados emple)
        {
            if (emple.IDEmp != 0)
            {
                return db.UpdateAsync(emple);
            }
            else
            {
                return db.InsertAsync(emple);
            }
        }
        public Task<List<Empleados>> GetEmpleadosAsync()
        {
            return db.Table<Empleados>().ToListAsync();
        }
        public Task<Empleados> GetEmpleadoByIdAsync(int IdEmp)
        {
            return db.Table<Empleados>().Where(a => a.IDEmp == IdEmp).FirstOrDefaultAsync();
        }
        public Task<int> DeleteEmpleadosAsync(Empleados empleado)
        {
            return db.DeleteAsync(empleado);
        }

        public Task<int> SaveCursosAsync(CursosE curso)
        {
            if (curso.IDCso != 0)
            {
                return db.UpdateAsync(curso);
            }
            else
            {
                return db.InsertAsync(curso);
            }
        }
        public Task<List<CursosE>> GetCursosAsync()
        {
            return db.Table<CursosE>().ToListAsync();
        }
        public Task<CursosE> GetCursosByIdAsync(int IdCso)
        {
            return db.Table<CursosE>().Where(b => b.IDCso == IdCso).FirstOrDefaultAsync();
        }
        public Task<int> DeleteCursosAsync(CursosE curso)
        {
            return db.DeleteAsync(curso);
        }

        public Task<int> SaveUserModelAsync(Users usr)
        {
            if (usr.UserId != 0)
            {
                return db.UpdateAsync(usr);
            }
            else
            {
                return db.InsertAsync(usr);
            }
        }
        public Task<List<Users>> GetUsersValidate(string email, string password)
        {
            return db.QueryAsync<Users>("SELECT * FROM Users WHERE EmailUser='" + email + "'AND EmailPassword='" + password + "'");
        }

        #region Tracking Course

        public Task<int> SaveCourseTracking(Seguimiento seguimiento)
        {
            try
            {
                if (seguimiento.IDSto != 0)
                {
                    return db.UpdateAsync(seguimiento);
                }
                else
                {
                    return db.InsertAsync(seguimiento);
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public Task<List<Seguimiento>> GetCoursesByEmployeeId(int employeeId)
        {
            try
            {
                return db.QueryAsync<Seguimiento>("SELECT * FROM Seguimiento WHERE IDEmp = " + employeeId.ToString());
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public Task<List<Seguimiento>> GetCourses()
        {
            return db.QueryAsync<Seguimiento>("SELECT * FROM Seguimiento ");
        }

        public Task<List<Seguimiento>> GetCourseByDate()
        {
            try
            {
                return db.QueryAsync<Seguimiento>("Select * from Seguimiento ");// where fechaCurso >= '" + DateTime.Now.Date + "'");
                                                    //+ " and strftime('%m', fechaCurso) = " + DateTime.Now.Date./*Month*/.ToString() 
                                                    //+ " and strftime('%d', fechaCurso) = " + DateTime.Now.Date.Day.ToString()); 
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public Task<Seguimiento> GetTrackingCourse(int IDSto)
        {
            try
            {
                 return db.Table<Seguimiento>().Where(a => a.IDSto == IDSto).FirstOrDefaultAsync();
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public Task<int> DeleteTrackingAsync(Seguimiento seguimiento)
        {
            return db.DeleteAsync(seguimiento);
        }


        #endregion


    }
}