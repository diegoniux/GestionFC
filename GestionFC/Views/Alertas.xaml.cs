﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Service = GestionFC.Services;
using GestionFC.Models.Log;


namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alertas : ContentPage
    {
        public ViewModels.PlantillaPage.PlantillaPageViewModel ViewModel { get; set; }
        private int nomina { get; set; }
        private string token { get; set; }
        public Master _master;
        public bool SesionExpired { get; set; }

        public Alertas()
        {
            InitializeComponent();
            SesionExpired = false;

            _master = (Master)App.MasterDetail.Master;

            ViewModel = new ViewModels.PlantillaPage.PlantillaPageViewModel();
            BindingContext = ViewModel;
            LoadPage();
            NavigationPage.SetHasNavigationBar(this, false);

            //Evento tap de la imagen hidepassword
            var burguerTap = new TapGestureRecognizer();
            burguerTap.Tapped += (object sender, EventArgs e) =>
            {
                App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
            };

            btnHamburguesa.GestureRecognizers.Add(burguerTap);
        }

        private async void LoadPage()
        {
            //logService = new Service.LogService();
            Service.HeaderService headerService = new Service.HeaderService();
            Service.PlantillaService gridPromotoresService = new Service.PlantillaService();
            try
            {
                nomina = App.Nomina;
                token = App.Token;

                if (nomina > 0)
                {
                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        await headerService.GetHeader(nomina, token).ContinueWith((Action<Task<Models.Share.HeaderResponseModel>>)(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {

                                // vericamos si la sesión expiró (token)
                                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                                {
                                    SesionExpired = true;
                                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                                }
                            }
                            //Cargar datos para el binding de información con el header
                            ViewModel.NombreGerente = x.Result.Progreso?.Nombre + " " + x.Result.Progreso?.Apellidos;
                            ViewModel.Mensaje = x.Result.Progreso?.Genero == "H" ? "¡Bienvenido!" : "¡Bienvenida!";
                            ViewModel.APsMetaAlcanzada = x.Result.APsMetaAlcanzada;
                            ViewModel.Plantilla = x.Result.Plantilla;
                            if (x.Result.Progreso != null)
                            {
                                ViewModel.Gerente = x.Result.Progreso;
                            }
                            _master.loadPage(nomina, ViewModel.NombreGerente, x.Result.Perfil, x.Result.Progreso.Foto, token);

                        }));

                    }
                    
                }
            }
            catch (Exception ex)
            {
                // Si la sesión expiró enviamos mensaje 
                if (SesionExpired)
                {
                    CerrarSesion();
                    return;
                }

                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 2,
                    Usuario = nomina,
                    Error = (ex.TargetSite == null ? "" : ex.TargetSite.Name + ". ") + ex.Message,
                    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name

                };
                await _master.logService.LogError(logError, "").ContinueWith(logRes =>
                {
                    if (logRes.IsFaulted)
                        DisplayAlert("Error", logRes.Exception.Message, "Ok");
                });
                await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
            }
        }

        void OnTapImageProductividad_Tapped(System.Object sender, System.EventArgs e)
        {
            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
            Device.BeginInvokeOnMainThread(() =>
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(ProductividadPage)));
                App.MasterDetail.IsPresented = false;
            });
        }

        void OnTapMenuHamburguesa_Tapped(System.Object sender, System.EventArgs e)
        {
            App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
        }


        async void CerrarSesion()
        {
            await DisplayAlert("Sesión Expirada.", "La sesión expiró, favor de ingresar nuevamente", "Ok");
            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
            Device.BeginInvokeOnMainThread(() =>
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(LoginPage)));
                App.MasterDetail.IsPresented = false;
            });
        }
    }
}