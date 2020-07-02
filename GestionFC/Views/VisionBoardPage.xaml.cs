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
                    await visionBoardService.GetMetaPlantillaIndividual(nomina,token).ContinueWith(x =>
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
                    IdPantalla = 3,
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

        private void OnTapPlantilla_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            gridPlantilla.IsVisible = true;

            tabPlantilla.BackgroundColor = Xamarin.Forms.Color.FromHex("#64A70B");
            lblTabPlantilla.TextColor = Xamarin.Forms.Color.FromHex("#FFFFFF");

            tabIndividual.BackgroundColor = Xamarin.Forms.Color.FromHex("#F3F3F3");
            lblTabIndividual.TextColor = Xamarin.Forms.Color.FromHex("#707070");

            gridIndividual.IsVisible = false;
            CollecionViewMetasAP.IsVisible = false;

        }

        private async void OnTapIndividual_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                gridPlantilla.IsVisible = false;
                
                gridIndividual.IsVisible = true;
                CollecionViewMetasAP.IsVisible = true;

                tabPlantilla.BackgroundColor = Xamarin.Forms.Color.FromHex("#F3F3F3");
                lblTabPlantilla.TextColor = Xamarin.Forms.Color.FromHex("#707070");

                tabIndividual.BackgroundColor = Xamarin.Forms.Color.FromHex("#FFA400");
                lblTabIndividual.TextColor = Xamarin.Forms.Color.FromHex("#FFFFFF");

                //ViewModel = (VisionBoardViewModel)BindingContext;
                //// vsalidación para solo cargar la información cuando
                //if (ViewModel.ProductividadSemanal?.ResultDatos.Count > 0)
                //{
                //    return;
                //}

                ////Carga de productividad Semanal
                //using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                //{
                //    // Limpiamos la información actual
                //    ViewModel.ProductividadSemanal = null;

                //    Service.ProductividadService productividadService = new Service.ProductividadService();
                //    await productividadService.GetProduccionSemanal(nomina, 0, 0, token, new DateTime(1900,01,01),false).ContinueWith(x =>
                //    {
                //        if (x.IsFaulted)
                //        {
                //            throw x.Exception;
                //        }

                //        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                //        {
                //            // vericamos si la sesión expiró (token)
                //            if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                //            {
                //                SesionExpired = true;
                //                throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                //            }
                //        }

                //        ViewModel.ProductividadSemanal = x.Result;
                //    });
                //}
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
                    IdPantalla = 3,
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

        private async void OnTapPrev_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                // Dependiendo del tipo de producción activada (Diaria o Semanal), cargamos el periodo anterior
                Service.ProductividadService productividadService = new Service.ProductividadService();
                bool EsPosterior = false;
                DateTime FechaCorte;
                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    //if (gridProdDiaria.IsVisible)
                    //{
                    //    //Carga de productividad Diaria
                    //    int anio = ViewModel.ProductividadDiaria.ResultAnioSemana.Anio;
                    //    int semanaAnio = ViewModel.ProductividadDiaria.ResultAnioSemana.SemanaAnio - 1;
                    //    FechaCorte = ViewModel.ProductividadDiaria.ResultAnioSemana.FechaCorte;

                    //    // si nos encontrabamos en la primer semana del año, nos movemos a la ulutima semana del año anterior
                    //    if (semanaAnio == 0)
                    //    {
                    //        semanaAnio = 52;
                    //        anio -= 1;
                    //    }

                    //    // Limpiamos la selección y el contenido
                    //    //CollecionViewProdDiaria.SelectedItem = null;
                    //    ViewModel.ProductividadDiaria = null;
                    //    await productividadService.GetProduccionDiaria(nomina, anio, semanaAnio, token, FechaCorte, EsPosterior).ContinueWith(x =>
                    //    {
                    //        if (x.IsFaulted)
                    //        {
                    //            throw x.Exception;
                    //        }

                    //        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                    //        {
                    //            // vericamos si la sesión expiró (token)
                    //            if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                    //            {
                    //                SesionExpired = true;
                    //                throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                    //            }
                    //        }

                    //        if (x.Result.ResultAnioSemana.EsUltimaFechaCorte)
                    //        {
                    //            Device.BeginInvokeOnMainThread(() =>
                    //            {
                    //                DisplayAlert("Mensaje", x.Result.ResultadoEjecucion.ErrorMessage, "Ok");
                    //            });

                    //        }

                    //        ViewModel.ProductividadDiaria = x.Result;

                    //    });
                    //}
                    //// cargar periodo anterior Semanal
                    //else
                    //{
                    //    int anio = ViewModel.ProductividadSemanal.ResultTotal.Anio;
                    //    int tetrasemanaAnio = ViewModel.ProductividadSemanal.ResultTotal.TetrasemanaAnio - 1;
                    //    FechaCorte = ViewModel.ProductividadSemanal.ResultTotal.FechaCorte;

                    //    if (tetrasemanaAnio == 0)
                    //    {
                    //        tetrasemanaAnio = 13;
                    //        anio -= 1;
                    //    }

                    //    // limpiamos la información actial y la selección, solo si carga bien 
                    //    CollecionViewMetasAP.SelectedItem = null;
                    //    ViewModel.ProductividadSemanal = null;

                    //    await productividadService.GetProduccionSemanal(nomina, anio, tetrasemanaAnio, token, FechaCorte, EsPosterior).ContinueWith(x =>
                    //    {
                    //        if (x.IsFaulted)
                    //        {
                    //            throw x.Exception;
                    //        }

                    //        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                    //        {
                    //            throw new Exception("Ocurrió un error");
                    //        }

                    //        if (x.Result.ResultTotal.EsUltimaFechaCorte)
                    //        {
                    //            Device.BeginInvokeOnMainThread(() =>
                    //            {
                    //                DisplayAlert("Mensaje", x.Result.ResultadoEjecucion.ErrorMessage, "Ok");
                    //            });

                    //        }

                    //        ViewModel.ProductividadSemanal = x.Result;



                    //    });
                    //}
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
                    IdPantalla = 3,
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

        private async void OnTapNext_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                // Dependiendo del tipo de producción activada (Diaria o Semanal), cargamos el periodo siguiente
                Service.ProductividadService productividadService = new Service.ProductividadService();
                Boolean EsPosterior = true;
                DateTime FechaCorte;
                //if (gridProdDiaria.IsVisible)
                //{

                //    // Validamos si se encuentra en el periodo actual, enviamos mensaje indicando que no se pueden consultar periodos
                //    // mayores al actual
                //    if (ViewModel.ProductividadDiaria.ResultAnioSemana.EsActual)
                //    {
                //        await DisplayAlert("Aviso", "No es posible consultar información de periodos mayores al actual.", "Ok");
                //        return;
                //    }


                //    //Carga de productividad Diaria
                //    int anio = ViewModel.ProductividadDiaria.ResultAnioSemana.Anio;
                //    int semanaAnio = ViewModel.ProductividadDiaria.ResultAnioSemana.SemanaAnio + 1;
                //    FechaCorte = ViewModel.ProductividadDiaria.ResultAnioSemana.FechaCorte;

                //    // El máximo de semana es 52, si llegamos a ese numero, asignamos la semana 1 del siguiente año
                //    if (semanaAnio == 53)
                //    {
                //        semanaAnio = 1;
                //        anio += 1;
                //    }

                //    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                //    {
                //        // Limpiamos la selección del content view

                //        // Limíamos la información actual
                //        //CollecionViewProdDiaria.SelectedItem = null;
                //        ViewModel.ProductividadDiaria = null;

                //        await productividadService.GetProduccionDiaria(nomina, anio, semanaAnio, token, FechaCorte, EsPosterior).ContinueWith(x =>
                //        {
                //            if (x.IsFaulted)
                //            {
                //                throw x.Exception;
                //            }

                //            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                //            {
                //                // vericamos si la sesión expiró (token)
                //                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                //                {
                //                    SesionExpired = true;
                //                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                //                }
                //            }

                //            ViewModel.ProductividadDiaria = x.Result;

                //        });
                //    }
                //}
                //// cargar periodo anterior Semanal
                //else
                //{
                //    //Si nos encontramos en la tretrasemana actual, mostramos mensaje indicando que no esposible consultar tetasemana futuras
                //    if (ViewModel.ProductividadSemanal.ResultTotal.EsActual)
                //    {
                //        await DisplayAlert("Aviso", "No es posible consultar información de periodos mayores al actual.", "Ok");
                //        return;
                //    }

                //    int anio = ViewModel.ProductividadSemanal.ResultTotal.Anio;
                //    int tetrasemanaAnio = ViewModel.ProductividadSemanal.ResultTotal.TetrasemanaAnio + 1;
                //    FechaCorte = ViewModel.ProductividadSemanal.ResultTotal.FechaCorte;


                //    // si nos encontrasmos en la ultiuma tetrasemana del año, nos movemos a la primer tetrasemana del próximo año
                //    if (tetrasemanaAnio == 14)
                //    {
                //        tetrasemanaAnio = 1;
                //        anio += 1;
                //    }

                //    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                //    {
                //        // Limíamos la información actual
                //        CollecionViewMetasAP.SelectedItem = null;
                //        ViewModel.ProductividadSemanal = null;

                //        await productividadService.GetProduccionSemanal(nomina, anio, tetrasemanaAnio, token, FechaCorte, EsPosterior).ContinueWith(x =>
                //        {
                //            if (x.IsFaulted)
                //            {
                //                throw x.Exception;
                //            }

                //            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                //            {
                //                // vericamos si la sesión expiró (token)
                //                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                //                {
                //                    SesionExpired = true;
                //                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                //                }
                //            }
                //            ViewModel.ProductividadSemanal = x.Result;
                //        });
                //    }
                //}
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
                    IdPantalla = 3,
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
            int meta = int.Parse(txtLunes.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoLunes)
            {
                ActualizarMetaDia(1, meta);
            }
        }

        void txtMartes_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtMartes.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMartes)
            {
                ActualizarMetaDia(2, meta);
            }
        }

        void txtMiercoles_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtMiercoles.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoMiercoles)
            {
                ActualizarMetaDia(3, meta);
            }
        }

        void txtJueves_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtJueves.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoJueves)
            {
                ActualizarMetaDia(4, meta);
            }
        }

        void txtViernes_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtViernes.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoViernes)
            {
                ActualizarMetaDia(5, meta);
            }
        }

        void txtSabado_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtSabado.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoSabado)
            {
                ActualizarMetaDia(6, meta);
            }
        }

        void txtDomingo_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            int meta = int.Parse(txtDomingo.Text);
            if (meta != ViewModel.GetMetaPlantilla.DetalleMetaPorDia.SaldoDomingo)
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

                    ActualizaPantalla();

                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", ex.Message, "Ok");
                });
            }

        }

        async void ActualizaPantalla()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.VisionBoardService visionBoardService = new Service.VisionBoardService();
            IsBusy = true;
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", ex.Message, "Ok");
                });
            }
        }

        async void MetaFolios_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                VisionBoardService service = new VisionBoardService();
                if (int.Parse(txtMetaTraspasos.Text) == ViewModel.GetMetaPlantilla.MetaPlantilla.Traspasos &&
                    int.Parse(txtMetaFCT.Text) == ViewModel.GetMetaPlantilla.MetaPlantilla.TraspasosFct)
                {
                    return;
                }

                var request = new MetaPlantillaFoliosRequestModel()
                {
                    MetaPlantillaFolios = new Models.Share.MetaPlantillaFoliosModel()
                    {
                        Nomina = App.Nomina,
                        Folios = int.Parse(txtMetaTraspasos.Text),
                        FoliosFct = int.Parse(txtMetaFCT.Text)
                    }
                };

                await service.RegistrarMetaPlantillaFolios(request, token).ContinueWith(x =>
                {
                    if (x.IsFaulted)
                    {
                        throw x.Exception;
                    }
                });

            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", ex.Message, "Ok");
                });
            }
        }
    }
}