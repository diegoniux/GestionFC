using Acr.UserDialogs;
using GestionFC.Models.Log;
using GestionFC.ViewModels.ProductividadPage;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Service = GestionFC.Services;

namespace GestionFC.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductividadPage : ContentPage
    {
        private int nomina;
        private string token;
        private bool isBusy = false;
        public ProductividadPageViewModel ViewModel { get; set; }
        private Master _master;

        public ProductividadPage()
        {
            InitializeComponent();
            _master = (Master)App.MasterDetail.Master;
            NavigationPage.SetHasNavigationBar(this, false);

            // Declaración del ViewModel y asignación al BindingContext
            ViewModel = new ProductividadPageViewModel();
            BindingContext = ViewModel;

            LoadPage();
        }

        private async void LoadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.ProductividadService productividadService = new Service.ProductividadService();
            IsBusy = true;
            
            try
            {
                await App.Database.GetGestionFCItemAsync().ContinueWith(x =>
                {
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
                    await headerService.GetHeader(nomina).ContinueWith(x =>
                     {
                         if (x.IsFaulted)
                         {
                             throw x.Exception;
                         }

                         if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                         {
                             throw new Exception("Ocurrió un error");
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

                    //Carga de productividad Diaria
                    await productividadService.GetProduccionDiaria(nomina, 0, 0, token).ContinueWith(x =>
                     {
                         if (x.IsFaulted)
                         {
                             throw x.Exception;
                         }

                         if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                         {
                             throw new Exception("Ocurrió un error");
                         }

                         ViewModel.ProductividadDiaria = x.Result;

                     });

                    //Carga de Comision Estimada
                    await productividadService.GetComisionEstimada(nomina, new DateTime(), token).ContinueWith(x =>
                    {
                        if (x.IsFaulted)
                        {
                            throw x.Exception;
                        }

                        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                        {
                            throw new Exception("Ocurrió un error");
                        }

                        ViewModel.ComisionEstimada = x.Result;
                    });

                    //Guardamos genramos la inserción en bitácora (acceso de pantalla)
                    var logModel = new LogSistemaModel()
                    {
                        IdPantalla = 3,
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

        private void OnTapProdDiaria_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            imgTabProdDiaria.Source = "prod_diaria_verde.png";
            imgTabProdSemanal.Source = "prod_sem_blanco.png";
            gridProdDiaria.IsVisible = true;
            CollecionViewProdDiaria.IsVisible = true;
            gridProdSemanal.IsVisible = false;
            CollecionViewProdSemanal.IsVisible = false;

            //Llamar método para cargar la productividad Diaria
        }

        private async void OnTapProdSemanal_Tapped(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                imgTabProdDiaria.Source = "prod_diaria_blanco.png";
                imgTabProdSemanal.Source = "prod_sem_naranja.png";
                gridProdDiaria.IsVisible = false;
                CollecionViewProdDiaria.IsVisible = false;
                gridProdSemanal.IsVisible = true;
                CollecionViewProdSemanal.IsVisible = true;

                ViewModel = (ProductividadPageViewModel)BindingContext;
                // vsalidación para solo cargar la información cuando
                if (ViewModel.ProductividadSemanal?.ResultDatos.Count > 0)
                {
                    return;
                }

                //Carga de productividad Semanal
                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    // Limpiamos la información actual
                    ViewModel.ProductividadSemanal = null;

                    Service.ProductividadService productividadService = new Service.ProductividadService();
                    await productividadService.GetProduccionSemanal(nomina, 0, 0, token).ContinueWith(x =>
                    {
                        if (x.IsFaulted)
                        {
                            throw x.Exception;
                        }

                        if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                        {
                            throw new Exception("Ocurrió un error");
                        }
                       
                        ViewModel.ProductividadSemanal = x.Result;
                    });
                }
            }
            catch (Exception ex)
            {
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
                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    if (gridProdDiaria.IsVisible)
                    {
                        //Carga de productividad Diaria
                        int anio = ViewModel.ProductividadDiaria.ResultAnioSemana.Anio;
                        int semanaAnio = ViewModel.ProductividadDiaria.ResultAnioSemana.SemanaAnio - 1;

                        // si nos encontrabamos en la primer semana del año, nos movemos a la ulutima semana del año anterior
                        if (semanaAnio == 0)
                        {
                            semanaAnio = 52;
                            anio -= 1;
                        }

                        ViewModel.ProductividadDiaria = null;
                        await productividadService.GetProduccionDiaria(nomina, anio, semanaAnio, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                throw new Exception("Ocurrió un error");
                            }

                            ViewModel.ProductividadDiaria = x.Result;

                        });
                    }
                    // cargar periodo anterior Semanal
                    else
                    {
                        int anio = ViewModel.ProductividadSemanal.ResultTotal.Anio;
                        int tetrasemanaAnio = ViewModel.ProductividadSemanal.ResultTotal.TetrasemanaAnio - 1;

                        if (tetrasemanaAnio == 0)
                        {
                            tetrasemanaAnio = 13;
                            anio -= 1;
                        }

                        // limpiamos la información actial
                        ViewModel.ProductividadSemanal = null;

                        await productividadService.GetProduccionSemanal(nomina, anio, tetrasemanaAnio, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                throw new Exception("Ocurrió un error");
                            }
                            ViewModel.ProductividadSemanal = x.Result;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
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
                
                if (gridProdDiaria.IsVisible)
                {

                    // Validamos si se encuentra en el periodo actual, enviamos mensaje indicando que no se pueden consultar periodos
                    // mayores al actual
                    if (ViewModel.ProductividadDiaria.ResultAnioSemana.EsActual)
                    {
                        await DisplayAlert("Aviso", "No es posible consultar información de periodos mayores al actual.", "Ok");
                        return;
                    }


                    //Carga de productividad Diaria
                    int anio = ViewModel.ProductividadDiaria.ResultAnioSemana.Anio;
                    int semanaAnio = ViewModel.ProductividadDiaria.ResultAnioSemana.SemanaAnio + 1;

                    // El máximo de semana es 52, si llegamos a ese numero, asignamos la semana 1 del siguiente año
                    if (semanaAnio == 53)
                    {
                        semanaAnio = 1;
                        anio += 1;
                    }

                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        // Limíamos la información actual
                        ViewModel.ProductividadDiaria = null;

                        await productividadService.GetProduccionDiaria(nomina, anio, semanaAnio, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                throw new Exception("Ocurrió un error");
                            }

                            ViewModel.ProductividadDiaria = x.Result;

                        });
                    }
                }
                // cargar periodo anterior Semanal
                else
                {
                    //Si nos encontramos en la tretrasemana actual, mostramos mensaje indicando que no esposible consultar tetasemana futuras
                    if (ViewModel.ProductividadSemanal.ResultTotal.EsActual)
                    {
                        await DisplayAlert("Aviso", "No es posible consultar información de periodos mayores al actual.", "Ok");
                        return;
                    }

                    int anio = ViewModel.ProductividadSemanal.ResultTotal.Anio;
                    int tetrasemanaAnio = ViewModel.ProductividadSemanal.ResultTotal.TetrasemanaAnio + 1;


                    // si nos encontrasmos en la ultiuma tetrasemana del año, nos movemos a la primer tetrasemana del próximo año
                    if (tetrasemanaAnio == 14)
                    {
                        tetrasemanaAnio = 1;
                        anio += 1;
                    }

                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        // Limíamos la información actual
                        ViewModel.ProductividadSemanal = null;

                        await productividadService.GetProduccionSemanal(nomina, anio, tetrasemanaAnio, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                throw new Exception("Ocurrió un error");
                            }
                            ViewModel.ProductividadSemanal = x.Result;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
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
    }
}