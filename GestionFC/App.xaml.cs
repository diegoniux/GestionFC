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
        public static int Nomina { get; set; }
        public static string Token { get; set; }
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
            get { return "https://desaiis01/spw_Api_GestionFC/"; }
            //get { return "http://192.168.15.29/"; }
        }

        public static string ClaveVersion
        {
            get { return "VersionGFC"; }
        }

        public App()
        {
            InitializeComponent();

            Xamarin.Essentials.VersionTracking.Track();

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
