using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using GestionFC.Models.DetalleEspecialista;
using GestionFC.Models.Log;
using GestionFC.Models.Share;
using GestionFC.Services;
using GestionFC.ViewModels.DetalleEspecialista;
using GestionFC.Views.PopupPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Service = GestionFC.Services;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEspecialistaPage : ContentPage
    {
        private int nomina { get; set; }
        private string token { get; set; }
        public int NominaAP { get; set; }
        public int EspecialistaIndex { get; set; }

        public DetalleEspecialistaPageViewModel ViewModel { get; set; }
        private Master _master;
        public bool SesionExpired { get; set; }

        
        public DetalleEspecialistaPage()
        {
            InitializeComponent();

            SesionExpired = false;
            _master = (Master)App.MasterDetail.Master;

            // Declaración del ViewModel y asignación al BindingContext
            ViewModel = new DetalleEspecialistaPageViewModel();
            BindingContext = ViewModel;
            LoadPage();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void LoadPage()
        {
            Service.DetalleEspecialistaService detalleService = new Service.DetalleEspecialistaService();
            Service.AlertaService alertaService = new Service.AlertaService();

            try
            {
                nomina = App.Nomina;
                token = App.Token;
                

                NominaAP = App.NominaAP;
                IsBusy = true;

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await detalleService.GetDetalleEspecialista(NominaAP, nomina, token).ContinueWith(x =>
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

                        //Cargar datros para el binding de información del especialista
                        ViewModel.DetalleEspecialista = x.Result;

                    });

                    //Carga de productividad Diaria
                    await alertaService.GetAlertaPlantillaImproductiva(nomina, token, NominaAP).ContinueWith(x =>
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

                        ViewModel.AlarmaVisible = x.Result.cantidad > 0;
                        ViewModel.LikeVisible = x.Result.cantidad == 0;

                        if (x.Result.cantidad > 0)
                        {
                            ViewModel.AlarmaImproductivo = x.Result.ResultDatos[0];
                        }
                        
                    });

                    await detalleService.GetDetalleEspecialistaHistorico(NominaAP, DateTime.Today.ToString("MM-dd-yyyy"), token).ContinueWith(x =>
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

                            throw new Exception(x.Result.ResultadoEjecucion.ErrorMessage);
                        }

                        ViewModel.DetalleHistorico = x.Result;
                    });


                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ViewModel.getLocation().ContinueWith(loc => {
                            //Guardamos genramos la inserción en bitácora (Cierre Sesión)
                            var logModel = new LogSistemaModel()
                            {
                                IdPantalla = 8,
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

                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
                    return;
                }

                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 8,
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
                IsBusy = false;
            }
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

        async void ImgProductividad_Tapped(System.Object sender, System.EventArgs e)
        {
            await App.MasterDetail.Detail.Navigation.PopAsync();
        }

        async void SearchCircleButton_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {

                if (txtFolioSolicitud.Text == null)
                {
                    return;
                }

                IsBusy = true;

                // Validaos el Folio de la solicitud seleccionado
                string FolioSolicitud = txtFolioSolicitud.Text.Trim();
                Service.DetalleEspecialistaService detalleService = new Service.DetalleEspecialistaService();
                var DetalleFolioResponse = new DetalleFolioResponseModel();

                if (FolioSolicitud.Length > 0)
                {

                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        await detalleService.GetDetalleFolio(FolioSolicitud, token).ContinueWith(x =>
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

                                throw new Exception(x.Result.ResultadoEjecucion.ErrorMessage);
                            }
                            else
                            {
                                if (x.Result.DetalleFolios.RegistroTraspasoId > 0)
                                {
                                    DetalleFolioResponse = x.Result;
                                    App.DetalleFolio = DetalleFolioResponse;


                                    DetalleFolioPopupPage DetalleFolioPage = new DetalleFolioPopupPage();

                                    PopupNavigation.Instance.PushAsync(DetalleFolioPage);

                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("Folio no encontrado", "No se encontró ninguna solicitud con el folio capturado, favor de revisar.", "Ok");
                                    });
                                }
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
                    return;
                }
                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 8,
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
                IsBusy = false;
            }
        }

        async void NextImage_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                if (!ViewModel.DetalleHistorico.DetalleHistoricoHeader.HabilitarAdelantar)
                {
                    return;
                }

                IsBusy = true;

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    var detalleService = new DetalleEspecialistaService();
                    await detalleService.GetDetalleEspecialistaHistorico(NominaAP, ViewModel.DetalleHistorico.DetalleHistoricoHeader.FechaAdelantar.ToString("MM-dd-yyyy"), token).ContinueWith(x =>
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

                            throw new Exception(x.Result.ResultadoEjecucion.ErrorMessage);
                        }

                        ViewModel.DetalleHistorico = x.Result;
                    });
                }
            }
            catch (Exception ex)
            {
                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
                    return;
                }
                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 8,
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
                IsBusy = false;
            }
        }

        async void PrevImage_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                if (!ViewModel.DetalleHistorico.DetalleHistoricoHeader.HabilitarAnterior)
                {
                    return;
                }

                IsBusy = true;

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    var detalleService = new DetalleEspecialistaService();
                    await detalleService.GetDetalleEspecialistaHistorico(NominaAP, ViewModel.DetalleHistorico.DetalleHistoricoHeader.FechaAnterior.ToString("MM-dd-yyyy"), token).ContinueWith(x =>
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

                            throw new Exception(x.Result.ResultadoEjecucion.ErrorMessage);
                        }

                        ViewModel.DetalleHistorico = x.Result;
                    });
                }


            }
            catch (Exception ex)
            {
                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
                    return;
                }
                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 8,
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
                IsBusy = false;
            }
        }
    }
}
