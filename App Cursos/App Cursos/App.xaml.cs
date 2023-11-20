using App_Cursos.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Cursos
{
    public partial class App : Application
    {
        static DataBase db;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Login());
        }

        public static DataBase SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Empresa.db3"));
                }
                return db;
            }

        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}