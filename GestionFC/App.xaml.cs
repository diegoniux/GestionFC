using GestionFC.SqLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionFC
{
    public partial class App : Application
    {

        static GestionFCDataBase database;

        public static GestionFCDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new GestionFCDataBase();
                }
                return database;
            }
        }

        public static string BaseUrlApi
        {
            get { return "http://192.168.0.2/Api_GestionFC/"; }
        } 

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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
