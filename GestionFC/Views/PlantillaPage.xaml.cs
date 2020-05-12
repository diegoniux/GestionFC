using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class PlantillaPage : ContentPage
    {
        public ViewModels.PlantillaPage.PlantillaPageViewModel ViewModel { get; set; }
        private int nomina { get; set; }
        private string token { get; set; }
        public Master _master;
        //private Service.LogService logService { get; set; }

        public PlantillaPage()
        {
            InitializeComponent();
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
                
                //try
                //{
                //await App.Database.GetGestionFCItemAsync().ContinueWith(x => {
                //    if (x.IsFaulted)
                //        throw x.Exception;
                //    if (x.Result == null || x.Result.Count == 0)
                //        throw new Exception("SQLite is null or empty.");

                //    if (!string.IsNullOrEmpty(x.Result[0]?.TokenSesion))
                //    {
                //        token = x.Result[0].TokenSesion;
                //        nomina = x.Result[0].Nomina;
                //    }
                //});
                //}
                //catch (Exception ex)
                //{
                //    var logError = new Models.Log.LogErrorModel()
                //    {
                //        IdPantalla = 2,
                //        Usuario = nomina,
                //        Error = (ex.TargetSite == null ? "" : ex.TargetSite.Name + ". ") + ex.Message,
                //        Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name

                //    };
                //    await _master.logService.LogError(logError, "").ContinueWith(logRes =>
                //    {
                //        if (logRes.IsFaulted)
                //            DisplayAlert("Error", logRes.Exception.Message, "Ok");
                //    });
                //    await DisplayAlert("SQLite PlantillaPage Error", ex.Message, "Ok");
                //    await _master.cerrarSesionError();
                //}
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
                                throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                            }
                            //Cargar datros para el binding de información con el header
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

                        await gridPromotoresService.GetGridPromotores(nomina, token).ContinueWith(x =>
                          {
                              if (x.IsFaulted)
                              {
                                  throw x.Exception;
                              }

                              if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                              {
                                  throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                              }

                              ViewModel.Agentes = x.Result.Promotores;
                          });

                        //Guardamos genramos la inserción en bitácora (acceso de pantalla)
                        var logModel = new LogSistemaModel()
                        {
                            IdPantalla = 2,
                            IdAccion = 2,
                            Usuario = nomina,
                            Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name
                        };
                        await _master.logService.LogSistema(logModel, token).ContinueWith(logRes =>
                        {
                            if (logRes.IsFaulted)
                                throw logRes.Exception;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
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

        void OnTapPizarronDigital_Tapped(System.Object sender, System.EventArgs e)
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
    }
}