using GestionFC.Models.Share;
using GestionFC.SqLite;
using GestionFC.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionFC
{
    public partial class App : Application
    {

        static GestionFCDataBase database;
        public static MasterDetailPage MasterDetail { get; set; }

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
            get { return "https://desaiis01/Api_GestionFC/"; }
        } 

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
