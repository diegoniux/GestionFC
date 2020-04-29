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

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantillaPage : ContentPage
    {
        public ViewModels.PlantillaPage.PlantillaPageViewModel ViewModel { get; set; }
        public Master _master;
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
            Service.LogService logService = new Service.LogService();
            Service.HeaderService headerService = new Service.HeaderService();
            Service.PlantillaService gridPromotoresService = new Service.PlantillaService();
            int nomina = 0;
            try
            {
                string token = string.Empty;
                await App.Database.GetGestionFCItemAsync().ContinueWith(x => {
                    if (x.IsFaulted)
                    {
                        throw x.Exception;
                    }

                    if (!string.IsNullOrEmpty(x.Result[0]?.TokenSesion))
                    {
                        token = x.Result[0].TokenSesion;
                        nomina = x.Result[0].Nomina;
                    }
                });

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await headerService.GetHeader(nomina).ContinueWith((Action<Task<Models.Share.HeaderResponseModel>>)(x =>
                    {
                        //Cargar datros para el binding de información con el header
                        ViewModel.NombreGerente = x.Result.Progreso?.Nombre + " " + x.Result.Progreso?.Apellidos;
                        ViewModel.Mensaje = x.Result.Progreso?.Genero == "H" ? "¡Bienvenido!" : "¡Bienvenida!";
                        ViewModel.APsMetaAlcanzada = x.Result.APsMetaAlcanzada;
                        ViewModel.Plantilla = x.Result.Plantilla;
                        if (x.Result.Progreso != null)
                        {
                            ViewModel.Gerente = x.Result.Progreso;
                        }
                        _master.loadPage(nomina, ViewModel.NombreGerente, x.Result.Perfil, x.Result.Progreso.Foto);
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
                }
            }
            catch (Exception ex)
            {
                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 2,
                    Usuario = nomina,
                    Error = ex.Message,
                    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name

                };
                await logService.LogError(logError, "").ContinueWith(logRes =>
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