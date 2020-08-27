using GestionFC.Models.DetalleEspecialista;
using GestionFC.Models.Share;
using GestionFC.SqLite;
using GestionFC.Views;
using System;
using System.Collections.Generic;
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

        // Propiedades para la pantalla detalle de especialista
        public static List<Progreso> Especialistas { get; set; }
        public static int NominaAP { get; set; }

        // Propiedad para el binding de la pantalla Detalle Folio
        public static DetalleFolioResponseModel DetalleFolio { get; set; }

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
            get { return "https://spw.invercap.com.mx/spw_Api_GestionFC/"; }
            // get { return "https://desaiis01/spw_Api_GestionFC/"; }
        }

        public static string ClaveVersion
        {
            get { return "VersionGFC"; }
        }

        public static string ClaveVersionQA
        {
            get { return "VersionGFCQA"; }
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
