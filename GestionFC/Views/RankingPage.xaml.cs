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
    public partial class RankingPage : ContentPage
    {
        public ViewModels.RankingPage.RankingViewModel ViewModel { get; set; }
        private int nomina { get; set; }
        private string token { get; set; }
        public Master _master;
        public bool SesionExpired { get; set; }

        public RankingPage()
        {
            InitializeComponent();
            SesionExpired = false;

            _master = (Master)App.MasterDetail.Master;

            ViewModel = new ViewModels.RankingPage.RankingViewModel();
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

                            ViewModel.TopGerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Victoria Janeth",
                                Apellidos = "Rosas Camacho",
                                Foto = "capi_circulo.png",
                                Posicion = "1er LUGAR",
                                Saldo = "$10,000,000",
                                TipoSaldo = "",
                                NumTraspaso = 10,
                                ImgPosicionSemAnt = "",
                                ColorPosicion = "#D5A73A",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_gold.png",
                                    semana2 = "icon_star_gold.png",
                                    semana3 = "icon_star_gold.png",
                                    semana4 = "icon_star_gold.png"
                                }
                            });
                            ViewModel.TopGerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Domingo Javier",
                                Apellidos = "Quintero Espinoza",
                                Foto = "capi_circulo.png",
                                Posicion = "2do",
                                Saldo = "$8,000,000",
                                TipoSaldo = "",
                                NumTraspaso = 8,
                                ImgPosicionSemAnt = "",
                                ColorPosicion = "#707070",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_plate.png",
                                    semana2 = "icon_star_plate.png",
                                    semana3 = "icon_star_plate.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });
                            ViewModel.TopGerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Marco Antonio",
                                Apellidos = "Pérez",
                                Foto = "capi_circulo.png",
                                Posicion = "3er",
                                Saldo = "$5,000,000",
                                TipoSaldo = "",
                                NumTraspaso = 6,
                                ImgPosicionSemAnt = "",
                                ColorPosicion = "#501313",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_bronze.png",
                                    semana3 = "icon_star_bronze.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });
                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Rene Alexander",
                                Apellidos = "Hernández de la Rosa",
                                Foto = "capi_circulo.png",
                                Posicion = "4",
                                Saldo = "$4,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 4,
                                ImgPosicionSemAnt = "icon_rank_up_green.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_plate.png",
                                    semana2 = "icon_star_bronze.png",
                                    semana3 = "icon_star_gold.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });
                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Mario Alberto",
                                Apellidos = "Villarreal",
                                Foto = "capi_circulo.png",
                                Posicion = "5",
                                Saldo = "$4,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 4,
                                ImgPosicionSemAnt = "icon_rank_equal.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_bronze.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });

                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "José Antonio",
                                Apellidos = "Rodríguez",
                                Foto = "capi_circulo.png",
                                Posicion = "6",
                                Saldo = "$4,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 4,
                                ImgPosicionSemAnt = "icon_rank_equal.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_bronze.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });

                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "José Luis",
                                Apellidos = "Garcia",
                                Foto = "capi_circulo.png",
                                Posicion = "7",
                                Saldo = "$4,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 4,
                                ImgPosicionSemAnt = "icon_rank_down_red.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_bronze.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });

                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "José Saul",
                                Apellidos = "Torres",
                                Foto = "capi_circulo.png",
                                Posicion = "8",
                                Saldo = "$3,400,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 4,
                                ImgPosicionSemAnt = "icon_rank_down_red.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_bronze.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });

                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Micaela",
                                Apellidos = "Duarte Gaitán",
                                Foto = "capi_circulo.png",
                                Posicion = "9",
                                Saldo = "$3,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 5,
                                ImgPosicionSemAnt = "icon_rank_down_red.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_bronze.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_grey.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });

                            ViewModel.Gerentes.Add(new Models.Share.RankingGte
                            {
                                Nombre = "Erika Sarahi",
                                Apellidos = "Guajardo Martínez",
                                Foto = "capi_circulo.png",
                                Posicion = "10",
                                Saldo = "$2,000,000",
                                TipoSaldo = "SALDO VIRTUAL",
                                NumTraspaso = 2,
                                ImgPosicionSemAnt = "icon_rank_down_red.png",
                                ColorPosicion = "",
                                Estrellas = new Models.Share.RankEstrellas
                                {
                                    semana1 = "icon_star_grey.png",
                                    semana2 = "icon_star_grey.png",
                                    semana3 = "icon_star_grey.png",
                                    semana4 = "icon_star_grey.png"
                                }
                            });
                        });
                        
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
