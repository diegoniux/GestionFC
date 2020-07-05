using Acr.UserDialogs;
using GestionFC.Models.Log;
using GestionFC.ViewModels.ProductividadPage;
using GestionFC.ViewModels.VisionBoard;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Service = GestionFC.Services;
using System.Drawing;
using GestionFC.Models.VisionBoard;
using GestionFC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionFC.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisionBoardPage : ContentPage
    {
        private int nomina { get; set; }

        private string token { get; set; }
        private bool isBusy = false;
        public VisionBoardViewModel ViewModel { get; set; }
        private Master _master;
        public bool SesionExpired { get; set; }
        public GetMetaPlantillaResponseModel MetaPlantilla { get; set; }
        public GetMetaPlantillaIndividualResponseModel MetaPlantillaIndividual { get; set; }

        public VisionBoardPage()
        {
            InitializeComponent();
            SesionExpired = false;
            _master = (Master)App.MasterDetail.Master;
            NavigationPage.SetHasNavigationBar(this, false);

            // Declaración del ViewModel y asignación al BindingContext
            ViewModel = new VisionBoardViewModel();
            BindingContext = ViewModel;

            LoadPage();
        }

        private async void LoadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.VisionBoardService visionBoardService = new Service.VisionBoardService();
            IsBusy = true;

            try
            {
                nomina = App.Nomina;
                token = App.Token;

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

                        //Cargar datros para el binding de información con el header
                        ViewModel.NombreGerente = x.Result.Progreso?.Nombre + " " + x.Result.Progreso?.Apellidos;
                        ViewModel.Mensaje = x.Result.Progreso?.Genero == "H" ? "¡Bienvenido!" : "¡Bienvenida!";
                        ViewModel.APsMetaAlcanzada = x.Result.APsMetaAlcanzada;
                        ViewModel.Plantilla = x.Result.Plantilla;
                        if (x.Result.Progreso != null)
                        {
                            ViewModel.Gerente = x.Result.Progreso;
                        }

                    });

                    //Carga de Meta Plantilla
                    await visionBoardService.GetMetaPlantilla(nomina, token).ContinueWith(x =>
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

                        ViewModel.GetMetaPlantilla = x.Result;

                    });

                    //Carga de Plantilla Individual
                    //await visionBoardService.GetMetaPlantillaIndividual(nomina,token).ContinueWith(x =>
                    //{
                    //    if (x.IsFaulted)
                    //    {
                    //        throw x.Exception;
                    //    }

                    //    if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                    //    {
                    //        // vericamos si la sesión expiró (token)
                    //        if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                    //        {
                    //            SesionExpired = true;
                    //            throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                    //        }
                    //    }

                    //    ViewModel.GetMetaPlantillaIndividual = x.Result;
                    //});

                    //Guardamos genramos la inserción en bitácora (acceso de pantalla)
                    var logModel = new LogSistemaModel()
                    {
                        IdPantalla = 6,
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
                    IdPantalla = 6,
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
                MetaPlantilla = new GetMetaPlantillaResponseModel()
                {
                    MetaPlantilla = new Models.Share.MetaPlantillaModel()
                    {
                        SaldoAcumuladoMeta = ViewModel.GetMetaPlantilla.MetaPlantilla.SaldoAcumuladoMeta,
                        ComisionSem = ViewModel.GetMetaPlantilla.MetaPlantilla.ComisionSem
                    },
                    DetalleMetaPorDia = new Models.Share.MetaDiaModel()
                    {
                        SaldoLunes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoLunes,
                        SaldoMartes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMartes,
                        SaldoMiercoles = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMiercoles,
                        SaldoJueves = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoJueves,
                        SaldoViernes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoViernes,
                        SaldoSabado = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoSabado,
                        SaldoDomingo = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoDomingo
                    }

                };

                IsBusy = false;
            }
        }

        //Para detectar el giro de pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "preventLandScape");
        }

        private void OnTapImageProductividad_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
            Device.BeginInvokeOnMainThread(() =>
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(PlantillaPage)));
                App.MasterDetail.IsPresented = false;
            });
        }

        private void OnTapMenuHamburguesa_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
        }

        private async void OnTapPlantilla_Tapped(object sender, EventArgs e)
        {

            try
            {

                gridPlantilla.IsVisible = true;

                tabPlantilla.BackgroundColor = Xamarin.Forms.Color.FromHex("#64A70B");
                lblTabPlantilla.TextColor = Xamarin.Forms.Color.FromHex("#FFFFFF");

                tabIndividual.BackgroundColor = Xamarin.Forms.Color.FromHex("#CBCBCB");
                lblTabIndividual.TextColor = Xamarin.Forms.Color.FromHex("#707070");

                gridIndividual.IsVisible = false;
                CollecionViewMetasAP.IsVisible = false;


                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    //Carga de Meta Plantilla
                    VisionBoardService service = new VisionBoardService();
                    await service.GetMetaPlantilla(nomina, token).ContinueWith(x =>
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

                        ViewModel.GetMetaPlantilla = x.Result;

                    });
                }
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
                    IdPantalla = 6,
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

        private async void OnTapIndividual_Tapped(object sender, EventArgs e)
        {
            try
            {
                gridPlantilla.IsVisible = false;

                gridIndividual.IsVisible = true;
                CollecionViewMetasAP.IsVisible = true;

                tabPlantilla.BackgroundColor = Xamarin.Forms.Color.FromHex("#CBCBCB");
                lblTabPlantilla.TextColor = Xamarin.Forms.Color.FromHex("#707070");

                tabIndividual.BackgroundColor = Xamarin.Forms.Color.FromHex("#FFA400");
                lblTabIndividual.TextColor = Xamarin.Forms.Color.FromHex("#FFFFFF");

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    //Carga de Meta Plantilla
                    VisionBoardService service = new VisionBoardService();
                    await service.GetMetaPlantillaIndividual(nomina, token).ContinueWith(x =>
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

                        ViewModel.GetMetaPlantillaIndividual = x.Result;

                    });
                }
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
                    IdPantalla = 6,
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
                MetaPlantillaIndividual = new GetMetaPlantillaIndividualResponseModel()
                {
                    ListMetaAp = new List<Models.Share.MetaAP>()
                };
                MetaPlantillaIndividual.ListMetaAp = new List<Models.Share.MetaAP>();
                foreach (var item in ViewModel.GetMetaPlantillaIndividual.ListMetaAp)
                {
                    MetaPlantillaIndividual.ListMetaAp.Add(new Models.Share.MetaAP()
                    {
                        Nomina = item.Nomina,
                        SaldoMeta = item.SaldoMeta
                    });
                }

                isBusy = false;
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

        void txtLunes_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtLunes.Text.Trim() == "")
            {
                txtLunes.Text = "0";
            }

            int meta = int.Parse(txtLunes.Text);

            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoLunes)
            {
                ActualizarMetaDia(1, meta);
            }

        }

        void txtMartes_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtMartes.Text.Trim() == "")
            {
                txtMartes.Text = "0";
            }

            int meta = int.Parse(txtMartes.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoMartes)
            {
                ActualizarMetaDia(2, meta);
            }
        }

        void txtMiercoles_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtMiercoles.Text.Trim() == "")
            {
                txtMiercoles.Text = "0";
            }
            int meta = int.Parse(txtMiercoles.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoMiercoles)
            {
                ActualizarMetaDia(3, meta);
            }
        }

        void txtJueves_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtJueves.Text.Trim() == "")
            {
                txtJueves.Text = "0";
            }
            int meta = int.Parse(txtJueves.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoJueves)
            {
                ActualizarMetaDia(4, meta);
            }
        }

        void txtViernes_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtViernes.Text.Trim() == "")
            {
                txtViernes.Text = "0";
            }
            int meta = int.Parse(txtViernes.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoViernes)
            {
                ActualizarMetaDia(5, meta);
            }
        }

        void txtSabado_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtSabado.Text.Trim() == "")
            {
                txtSabado.Text = "0";
            }
            int meta = int.Parse(txtSabado.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoSabado)
            {
                ActualizarMetaDia(6, meta);
            }
        }

        void txtDomingo_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (txtDomingo.Text.Trim() == "")
            {
                txtDomingo.Text = "0";
            }
            int meta = int.Parse(txtDomingo.Text);
            if (meta != MetaPlantilla.DetalleMetaPorDia.SaldoDomingo)
            {
                ActualizarMetaDia(7, meta);
            }
        }

        async void ActualizarMetaDia(int dia, int meta)
        {
            try
            {
                VisionBoardService service = new VisionBoardService();

                var request = new MetaPlantillaRequestModel()
                {
                    MetaPlantillaDia = new Models.Share.MetaPlantillaDiaModel()
                    {
                        IdPeriodo = 1,
                        Nomina = App.Nomina,
                        SaldoAcumuladoMeta = meta,
                        Dia = dia
                    }
                };


                await service.RegistrarMetaPlantilla(request, token).ContinueWith(x =>
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
                });

                await ActualizaPantalla();

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
                    IdPantalla = 6,
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

        async Task<bool> ActualizaPantalla()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.VisionBoardService visionBoardService = new Service.VisionBoardService();
            IsBusy = true;
            bool Result = true;
            try
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

                    //Cargar datros para el binding de información con el header
                    ViewModel.NombreGerente = x.Result.Progreso?.Nombre + " " + x.Result.Progreso?.Apellidos;
                    ViewModel.Mensaje = x.Result.Progreso?.Genero == "H" ? "¡Bienvenido!" : "¡Bienvenida!";
                    ViewModel.APsMetaAlcanzada = x.Result.APsMetaAlcanzada;
                    ViewModel.Plantilla = x.Result.Plantilla;
                    if (x.Result.Progreso != null)
                    {
                        ViewModel.Gerente = x.Result.Progreso;
                    }

                });

                //Carga de Meta Plantilla
                await visionBoardService.GetMetaPlantilla(nomina, token).ContinueWith(x =>
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

                    ViewModel.GetMetaPlantilla = x.Result;

                });

                //Carga de Plantilla Individual
                await visionBoardService.GetMetaPlantillaIndividual(nomina, token).ContinueWith(x =>
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

                    ViewModel.GetMetaPlantillaIndividual = x.Result;
                });
            }
            catch (Exception ex)
            {

                if (SesionExpired)
                {
                    CerrarSesion();
                    isBusy = false;
                    Result = false;
                }

                var logError = new Models.Log.LogErrorModel()
                {
                    IdPantalla = 6,
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
                MetaPlantilla = new GetMetaPlantillaResponseModel()
                {
                    MetaPlantilla = new Models.Share.MetaPlantillaModel()
                    {
                        SaldoAcumuladoMeta = ViewModel.GetMetaPlantilla.MetaPlantilla.SaldoAcumuladoMeta,
                        ComisionSem = ViewModel.GetMetaPlantilla.MetaPlantilla.ComisionSem
                    },
                    DetalleMetaPorDia = new Models.Share.MetaDiaModel()
                    {
                        SaldoLunes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoLunes,
                        SaldoMartes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMartes,
                        SaldoMiercoles = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMiercoles,
                        SaldoJueves = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoJueves,
                        SaldoViernes = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoViernes,
                        SaldoSabado = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoSabado,
                        SaldoDomingo = ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoDomingo
                    }

                };
                IsBusy = false;
            }
            return Result;
        }

        async void MetaFolios_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                VisionBoardService service = new VisionBoardService();
                if (int.Parse(txtMetaTraspasos.Text) == MetaPlantilla.MetaPlantilla.Traspasos &&
                    int.Parse(txtMetaFCT.Text) == MetaPlantilla.MetaPlantilla.TraspasosFct)
                {
                    return;
                }

                if (txtMetaTraspasos.Text.Length <= 0 || txtMetaFCT.Text.Length <= 0)
                    return;

                var request = new MetaPlantillaFoliosRequestModel()
                {
                    MetaPlantillaFolios = new Models.Share.MetaPlantillaFoliosModel()
                    {
                        Nomina = App.Nomina,
                        Folios = int.Parse(txtMetaTraspasos.Text),
                        FoliosFct = int.Parse(txtMetaFCT.Text)
                    }
                };

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await service.RegistrarMetaPlantillaFolios(request, token).ContinueWith(x =>
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

                    });
                }

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
                    IdPantalla = 6,
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

        async void txtMetaSaldo_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                isBusy = true;
                var entry = (Entry)sender;
                if (entry.Text.Length <= 0)
                {
                    IsBusy = false;
                    return;
                }

                var NominaAP = int.Parse(entry.ClassId);
                var MetaAP = MetaPlantillaIndividual.ListMetaAp.Find(item => item.Nomina == NominaAP);

                var metaActual = int.Parse(MetaAP.SaldoMeta.Replace("$", "").Replace(",", ""));
                var nuevaMeta = int.Parse(entry.Text.Replace("$", "").Replace(",", ""));

                


                VisionBoardService service = new VisionBoardService();
                if (nuevaMeta == metaActual)
                {
                    return;
                }

                var request = new MetaPlantillaIndividualRequestModel()
                {
                    MetaPlantillaIndividual = new Models.Share.MetaPlantillaIndividualModel()
                    {
                        Nomina = App.Nomina,
                        NominaAp = NominaAP,
                        SaldoAcumuladoMeta = nuevaMeta
                    }
                };

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await service.RegistrarMetaPlantillaIndividual(request, token).ContinueWith(x =>
                    {
                        if (x.IsFaulted)
                        {
                            throw x.Exception;
                        }

                        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                        {
                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                // vericamos si la sesión expiró (token)
                                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                                {
                                    SesionExpired = true;
                                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                                }
                            }
                        }

                        
                    });
                    await ActualizaPantalla();
                }

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
                    IdPantalla = 6,
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

        async void txtComisionSem_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                if (txtMetaComSem.Text.Length <= 0)
                {
                    return;
                }

                var entry = (Entry)sender;

                var comisionActual = int.Parse(MetaPlantilla.MetaPlantilla.ComisionSem.Replace("$", "").Replace(",", ""));

                var Comision = int.Parse(txtMetaComSem.Text.Trim().Replace("$", "").Replace(",", ""));

                if (comisionActual == Comision)
                {
                    return;
                }

                VisionBoardService service = new VisionBoardService();

                var request = new MetaPlantillaComisionSemRequestModel()
                {
                    MetaComisionSem = new Models.Share.MetaComisionSemModel()
                    {
                        IdPeriodo = 1,
                        Nomina = App.Nomina,
                        ComisionSem = Comision
                    }
                };

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await service.RegistrarMetaPlantillaComisionSem(request, token).ContinueWith(x =>
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

                    ActualizaPantalla();
                });
                }

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
                    IdPantalla = 6,
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

        async void txtTotal_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                //if (txtTotal.Text.Trim().Replace("$", "").Replace(",", "") == "")
                //{
                //    txtTotal.Text = "0";
                //    return;
                //}
                if (txtTotal.Text.Length <= 0)
                {
                    return;
                }
                var TotalSaldoAcumulado = int.Parse(txtTotal.Text.Trim().Replace("$", "").Replace(",", ""));


                VisionBoardService service = new VisionBoardService();

                var request = new MetaPlantillaSaldoAcumuladoRequestModel()
                {
                    MetaSaldoAcumulado = new Models.Share.MetaSaldoAcumuladoModel()
                    {
                        IdPeriodo = 1,
                        Nomina = App.Nomina,
                        SaldoAcumuladoMeta = int.Parse(txtTotal.Text.Trim().Replace("$", "").Replace(",", ""))
            }
                };

                if(request.MetaSaldoAcumulado.SaldoAcumuladoMeta < 5000000)
                {
                    await DisplayAlert("Mensajev", "Saldo acomulado debe ser mayor o igual a $5,000,000 ", "Ok");
                    request.MetaSaldoAcumulado.SaldoAcumuladoMeta = 5000000;
                }

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await service.RegistrarMetaPlantillaSaldoAcumulado(request, token).ContinueWith(x =>
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

                            ActualizaPantalla();
                        });
                }

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
                    IdPantalla = 6,
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
