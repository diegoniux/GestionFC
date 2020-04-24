using Acr.UserDialogs;
using GestionFC.ViewModels.ProductividadPage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Service = GestionFC.Services;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductividadPage : ContentPage
    {
        public ProductividadPageViewModel ViewModel { get; set; }

        public ProductividadPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            ViewModel = new ProductividadPageViewModel();

            LoadPage();

            //Evento tap de la imagen hidepassword
            var burguerTap = new TapGestureRecognizer();
            burguerTap.Tapped += (object sender, EventArgs e) =>
            {
                App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
            };

            btnHamburguesa.GestureRecognizers.Add(burguerTap);

            //Evento tap del TabProdDiaria
            var ProdDiariaTap = new TapGestureRecognizer();
            ProdDiariaTap.Tapped += (object sender, EventArgs e) =>
            {
                imgTabProdDiaria.Source = "prod_diaria_verde.png";
                imgTabProdSemanal.Source = "prod_sem_blanco.png";
                gridProdDiaria.IsVisible = true;
                gridProdSemanal.IsVisible = false;
                
                //Llamar método para cargar la productividad Diaria


            };

            //Evento tap del TabProdSemanal
            var ProdSemTap = new TapGestureRecognizer();
            ProdSemTap.Tapped += (object sender, EventArgs e) =>
            {
                imgTabProdDiaria.Source = "prod_diaria_blanco.png";
                imgTabProdSemanal.Source = "prod_sem_naranja.png";
                gridProdDiaria.IsVisible = false;
                gridProdSemanal.IsVisible = true;

                //Llamar método para cargar la productividad Semanal


            };

            imgTabProdDiaria.GestureRecognizers.Add(ProdDiariaTap);
            imgTabProdSemanal.GestureRecognizers.Add(ProdSemTap);
        }

        private async void LoadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.PlantillaService gridPromotoresService = new Service.PlantillaService();
            Service.ProductividadService productividadService = new Service.ProductividadService();


            int nomina = 0;
            try
            {
                string token = string.Empty;
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

                        if (! x.Result.ResultadoEjecucion.EjecucionCorrecta)
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
                    await productividadService.GetComisionEstimada(nomina,new DateTime(), token).ContinueWith(x =>
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

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        BindingContext = ViewModel;
                    });

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
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
            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
            Device.BeginInvokeOnMainThread(() =>
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(PlantillaPage)));
                App.MasterDetail.IsPresented = false;
            });
        }

        private void OnTapMenuHamburguesa_Tapped(object sender, EventArgs e)
        {
            App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
        }
    }
}