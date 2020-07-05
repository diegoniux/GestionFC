using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Service = GestionFC.Services;
using GestionFC.Models.Log;
using GestionFC.ViewModels.AlertasPage;
using System.Collections.Generic;
using GestionFC.Models.Share;
using Newtonsoft.Json;
using System.Globalization;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alertas : ContentPage
    {
        public PlantillaImproductivaViewModel ViewModel { get; set; }
        private int nomina { get; set; }
        private string token { get; set; }
        private bool isBusy = false;
        public Master _master;
        public bool SesionExpired { get; set; }
        public DateTime Fecha { get; set; }
       
        
        public Alertas()
        {
            InitializeComponent();
            SesionExpired = false;
            Fecha = DateTime.Today;
            _master = (Master)App.MasterDetail.Master;

            
            LoadPage();
            NavigationPage.SetHasNavigationBar(this, false);
            
            //Evento tap de la imagen hidepassword
            var burguerTap = new TapGestureRecognizer();
            burguerTap.Tapped += (object sender, EventArgs e) =>
            {
                App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
            };
            // Declaración del ViewModel y asignación al BindingContext
            ViewModel = new PlantillaImproductivaViewModel();
            ViewModel.Dia = Fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es-MX")).UppercaseFirst();
            ViewModel.Fecha = $" {Fecha.ToString("dd", CultureInfo.CreateSpecificCulture("es-MX"))} de {Fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")).UppercaseFirst()} de {Fecha.ToString("yyyy")}";
            BindingContext = ViewModel;            
            
            btnHamburguesa.GestureRecognizers.Add(burguerTap);
        }

        private async void LoadPage()
        {
            
            //logService = new Service.LogService();
            Service.HeaderService headerService = new Service.HeaderService();
            Service.AlertaService alertaService = new Service.AlertaService();
            IsBusy = true;
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

                        await alertaService.GetAlertaPlantillaImproductiva(nomina, token).ContinueWith(x =>
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
                            ViewModel.PlantillaImproductiva = x.Result;
                        });

                        await alertaService.GetAlertaPlantillaSinSaldoVirtual(nomina, token).ContinueWith(x =>
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
                            ViewModel.FoliosPendientesSV = x.Result;
                            ViewModel.CreateCollection();
                            

                        });


                    }
                    pickerAP.Items.Add("TODOS");
                    foreach (FoliosPendientesSVModel a in ViewModel.sourcePicker)
                    {
                        pickerAP.Items.Add(a.Nombre);
                    }
                    notidicacionImp.Text = ViewModel.PlantillaImproductiva.cantidad.ToString();
                    notidicacionSV.Text = ViewModel.FoliosPendientesSV.cantidad.ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ViewModel.getLocation().ContinueWith(loc => {
                            //Guardamos genramos la inserción en bitácora (Cierre Sesión)
                            var logModel = new LogSistemaModel()
                            {
                                IdPantalla = 4,
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
                    IsBusy = false;
                    return;
                }

                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 4,
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
            finally
            {
                IsBusy = false;
            }
        }

        void OnTapImageProductividad_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isBusy) return;
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

        private void OnTapImproductividad_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            
            CambiaTab(1);
            CollecionViewImproductiva.IsVisible = true;
            CollecionViewRecuperacion.IsVisible = false;
            CollecionViewInvestigacion.IsVisible = false;
            CollecionViewPendientesSV.IsVisible = false;

            //Llamar método para cargar Improductividad
        }
        private void OnTapRecuperacion_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;

            CambiaTab(2);
            CollecionViewImproductiva.IsVisible = false;
            CollecionViewRecuperacion.IsVisible = true;
            CollecionViewInvestigacion.IsVisible = false;
            CollecionViewPendientesSV.IsVisible = false;

            //Llamar método para cargar Recuperacion
        }
        private void OnTapInvestigacion_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;

            CambiaTab(3);
            CollecionViewImproductiva.IsVisible = false;
            CollecionViewRecuperacion.IsVisible = false;
            CollecionViewInvestigacion.IsVisible = true;
            CollecionViewPendientesSV.IsVisible = false;

            //Llamar método para cargar Investigacion
        }
        private void OnTapPendientesSV_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;

            CambiaTab(4);
            CollecionViewImproductiva.IsVisible = false;
            CollecionViewRecuperacion.IsVisible = false;
            CollecionViewInvestigacion.IsVisible = false;
            CollecionViewPendientesSV.IsVisible = true;

            //Llamar método para cargar PendientesSV
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


        void CambiaTab(int tab)
        {

            string notifyGreen = "notification_circle_green.png";
            string notifyGray = "notification_circle_grey.png";
            string colorBoxGreen = "#71B417";
            string colorBoxGray = "#CBCBCB";
            string colorTextEnable = "#FFFFFF";
            string colorTextDisable = "#707070";
            switch (tab)
            {
                case 1:
                    FolioSearch.IsVisible = false;
                    PickerSV.IsVisible = false;
                    textoTitulo.Text = "Plantilla improductiva";
                    imgNotifyImproductiva.Source = notifyGreen;
                    //imgNotifyRecuperacion.Source = notifyGray;
                    //imgNotifyInvestigacion.Source = notifyGray;
                    imgNotifyFolioPendientesSV.Source = notifyGray;
                    BVPlantillaImproductiva.BackgroundColor = Color.FromHex(colorBoxGreen);
                    //BVPlantillaRecuperacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    //BVPlantillaInvestigacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    BVFoliosPendientesSV.BackgroundColor = Color.FromHex(colorBoxGray);
                    lblPlantillaImproductiva.TextColor = Color.FromHex(colorTextEnable);
                    //lblPlantillaRecuperacion.TextColor = Color.FromHex(colorTextDisable);
                    //lblPlantillaInvestigacion.TextColor = Color.FromHex(colorTextDisable);
                    lblFoliosPendientesSV.TextColor = Color.FromHex(colorTextDisable);
                    break;
                //case 2:
                //    FolioSearch.IsVisible = false;
                //    PickerSV.IsVisible = false;
                //    textoTitulo.Text = "Plantilla Recuperación";
                //    imgNotifyImproductiva.Source = notifyGray;
                //    imgNotifyRecuperacion.Source = notifyGreen;
                //    imgNotifyInvestigacion.Source = notifyGray;
                //    imgNotifyFolioPendientesSV.Source = notifyGray;
                //    BVPlantillaImproductiva.BackgroundColor = Color.FromHex(colorBoxGray);
                //    BVPlantillaRecuperacion.BackgroundColor = Color.FromHex(colorBoxGreen);
                //    BVPlantillaInvestigacion.BackgroundColor = Color.FromHex(colorBoxGray);
                //    BVFoliosPendientesSV.BackgroundColor = Color.FromHex(colorBoxGray);
                //    lblPlantillaImproductiva.TextColor = Color.FromHex(colorTextDisable);
                //    lblPlantillaRecuperacion.TextColor = Color.FromHex(colorTextEnable);
                //    lblPlantillaInvestigacion.TextColor = Color.FromHex(colorTextDisable);
                //    lblFoliosPendientesSV.TextColor = Color.FromHex(colorTextDisable);
                //    break;
                //case 3:
                //    FolioSearch.IsVisible = false;
                //    PickerSV.IsVisible = false;
                //    textoTitulo.Text = "Plantilla Investigación";
                //    imgNotifyImproductiva.Source = notifyGray;
                //    imgNotifyRecuperacion.Source = notifyGray;
                //    imgNotifyInvestigacion.Source = notifyGreen;
                //    imgNotifyFolioPendientesSV.Source = notifyGray;
                //    BVPlantillaImproductiva.BackgroundColor = Color.FromHex(colorBoxGray);
                //    BVPlantillaRecuperacion.BackgroundColor = Color.FromHex(colorBoxGray);
                //    BVPlantillaInvestigacion.BackgroundColor = Color.FromHex(colorBoxGreen);
                //    BVFoliosPendientesSV.BackgroundColor = Color.FromHex(colorBoxGray);
                //    lblPlantillaImproductiva.TextColor = Color.FromHex(colorTextDisable);
                //    lblPlantillaRecuperacion.TextColor = Color.FromHex(colorTextDisable);
                //    lblPlantillaInvestigacion.TextColor = Color.FromHex(colorTextEnable);
                //    lblFoliosPendientesSV.TextColor = Color.FromHex(colorTextDisable);
                //    break;
                case 4:
                    FolioSearch.IsVisible = true;
                    PickerSV.IsVisible = true;
                    textoTitulo.Text = "Folios pendientes Saldo Virtual";
                    imgNotifyImproductiva.Source = notifyGray;
                    //imgNotifyRecuperacion.Source = notifyGray;
                    //imgNotifyInvestigacion.Source = notifyGray;
                    imgNotifyFolioPendientesSV.Source = notifyGreen;
                    BVPlantillaImproductiva.BackgroundColor = Color.FromHex(colorBoxGray);
                    //BVPlantillaRecuperacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    //BVPlantillaInvestigacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    BVFoliosPendientesSV.BackgroundColor = Color.FromHex(colorBoxGreen);
                    lblPlantillaImproductiva.TextColor = Color.FromHex(colorTextDisable);
                    //lblPlantillaRecuperacion.TextColor = Color.FromHex(colorTextDisable);
                    //lblPlantillaInvestigacion.TextColor = Color.FromHex(colorTextDisable);
                    lblFoliosPendientesSV.TextColor = Color.FromHex(colorTextEnable);
                    break;
                default:
                    FolioSearch.IsVisible = false;
                    PickerSV.IsVisible = false;
                    textoTitulo.Text = "Plantilla improductiva";
                    imgNotifyImproductiva.Source = notifyGreen;
                    //imgNotifyRecuperacion.Source = notifyGray;
                    //imgNotifyInvestigacion.Source = notifyGray;
                    imgNotifyFolioPendientesSV.Source = notifyGray;
                    BVPlantillaImproductiva.BackgroundColor = Color.FromHex(colorBoxGreen);
                    //BVPlantillaRecuperacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    //BVPlantillaInvestigacion.BackgroundColor = Color.FromHex(colorBoxGray);
                    BVFoliosPendientesSV.BackgroundColor = Color.FromHex(colorBoxGray);
                    lblPlantillaImproductiva.TextColor = Color.FromHex(colorTextEnable);
                    //lblPlantillaRecuperacion.TextColor = Color.FromHex(colorTextDisable);
                    //lblPlantillaInvestigacion.TextColor = Color.FromHex(colorTextDisable);
                    lblFoliosPendientesSV.TextColor = Color.FromHex(colorTextDisable);
                    break;
            }
        }

        private async void OnTapFolioConSaldo_Tapped(object sender, EventArgs e)
        {
            bool limpiar = false;
            if (isBusy) return;
            isBusy = true;

            var idalerta = ((Image)sender).ClassId;
            Service.AlertaService alertaService = new Service.AlertaService();

            
            try
            {

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {

                    limpiar = await DisplayAlert("Mensaje", "¿Desea limpiar el registro?", "Aceptar", "Cancelar");
                    if (limpiar)
                    {
                        await alertaService.GetAlertaPlantillaSeguimientoSinSaldoVirtual(nomina, Convert.ToInt32(idalerta), token).ContinueWith(x =>
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
                            ViewModel.FoliosPendientesSV = x.Result;
                            //Device.BeginInvokeOnMainThread(() =>
                            //{
                            //    pickerAP.Items.Clear();
                            //});
                            //pickerAP.Items.Add("TODOS");
                            //foreach (FoliosPendientesSVModel a in ViewModel.sourcePicker)
                            //{
                            //    pickerAP.Items.Add(a.Nombre);
                            //}
                            //notidicacionImp.Text = ViewModel.PlantillaImproductiva.cantidad.ToString();
                            //notidicacionSV.Text = ViewModel.FoliosPendientesSV.cantidad.ToString();
                        });
                    }
                }
                notidicacionSV.Text = ViewModel.FoliosPendientesSV.cantidad.ToString();
            }
            catch (Exception ex)
            {
                if (SesionExpired)
                {
                    CerrarSesion();
                    isBusy = false;
                    return;
                }

                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 4,
                    Usuario = nomina,
                    Error = ex.Message,
                    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name
                };
                await _master.logService.LogError(logError, "").ContinueWith(logRes =>
                {
                    if (logRes.IsFaulted)
                        DisplayAlert("Error", logRes.Exception.Message, "Ok");
                });
                await DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                isBusy = false;
            }
        }

        private void SelectedIndexChangedName(object sender, EventArgs e)
        {
            var nombre = pickerAP.Items[pickerAP.SelectedIndex];
            ViewModel.FilterItemsNombre(nombre);
        }

        private void FolioFiltro(object sender, EventArgs e)
        {
            ViewModel.FilterItemsFolio(SearchFolio.Text);
        }

        private void button1_Clicked(object sender, EventArgs e)
        {
            pickerAP.Focus();
        }

    }
}
