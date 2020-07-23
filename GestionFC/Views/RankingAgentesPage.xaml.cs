using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Service = GestionFC.Services;
using Models = GestionFC.Models.Ranking;
using GestionFC.Models.Log;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankingAgentesPage : ContentPage
    {
        public ViewModels.RankingPage.RankingAgentesPageViewModel ViewModel { get; set; }
        private int nomina { get; set; }
        private string token { get; set; }
        public Master _master;
        public bool SesionExpired { get; set; }

        public RankingAgentesPage()
        {
            InitializeComponent();
            SesionExpired = false;

            _master = (Master)App.MasterDetail.Master;

            ViewModel = new ViewModels.RankingPage.RankingAgentesPageViewModel();
            BindingContext = ViewModel;
            LoadPage();
            NavigationPage.SetHasNavigationBar(this, false);

            var burguerTap = new TapGestureRecognizer();
            burguerTap.Tapped += (object sender, EventArgs e) =>
            {
                App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
            };
            btnHamburguesa.GestureRecognizers.Add(burguerTap);
        }

        private async void LoadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.RankingService rankingService = new Service.RankingService();
            try
            {
                nomina = App.Nomina;
                token = App.Token;

                if (nomina > 0)
                {
                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        await headerService.GetHeader(nomina, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {

                                // verificamos si la sesión expiró (token)
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
                        });
                    }
                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        await rankingService.GetRankingEspecialistas(nomina, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {

                                // verificamos si la sesión expiró (token)
                                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                                {
                                    SesionExpired = true;
                                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                                }
                            }
                            if (x.Result.TopEspecialistas != null)
                            {
                                ViewModel.TopEspecialistas = x.Result.TopEspecialistas;
                            }
                            if (x.Result.Especialistas != null)
                            {
                                ViewModel.Especialistas = x.Result.Especialistas;
                            }
                            ViewModel.Horas = x.Result.Horas;
                            ViewModel.Dias = x.Result.Dias;
                        });
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ViewModel.getLocation().ContinueWith(loc =>
                        {
                            //Guardamos genramos la inserción en bitácora (Cierre Sesión)
                            var logModel = new LogSistemaModel()
                            {
                                IdPantalla = 7,
                                IdAccion = 2,
                                Usuario = nomina,
                                Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name,
                                Geolocalizacion = loc.Result
                            };
                            _master.logService.LogSistema(logModel, token).ContinueWith(logRes =>
                            {
                                if (logRes.IsFaulted)
                                    throw logRes.Exception;
                            });
                        });
                    });
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
                    IdPantalla = 7,
                    Usuario = nomina,
                    Error = (ex.TargetSite == null ? "" : ex.TargetSite.Name + ". ") + ex.Message,
                    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name

                };
                await _master.logService.LogError(logError, "").ContinueWith(logRes =>
                {
                    if (logRes.IsFaulted)
                        DisplayAlert("Error", logRes.Exception.Message, "Ok");
                });
                await DisplayAlert("RankingPage Error", ex.Message, "Ok");
            }
        }

        void OnTapImageProductividad_Tapped(System.Object sender, System.EventArgs e)
        {
            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
            Device.BeginInvokeOnMainThread(() =>
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(PlantillaPage)));
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
