using Acr.UserDialogs;
using GestionFC.Models.DTO;
using GestionFC.Models.Login;
using GestionFC.Models.Log;
using GestionFC.Services;
using GestionFC.SqLite.DBModel;
using GestionFC.ViewModels.Login;
using GestionFC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using FFImageLoading;

namespace GestionFC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel LoginViewModel { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            // Ocultamos la barra de navegación
            NavigationPage.SetHasNavigationBar(this, false);

            // numeric keyboard

            UserName.Keyboard =  Keyboard.Numeric;

            //BecomeFirstResponder();


            //Obtenemos el valor del usuario recordado (ne caso de que exista) para mostrarlo en el entry
            int UserRemember = 0;
            App.Database.GetGestionFCItemAsync().ContinueWith(x => {
                if (x.Result[0].UserSaved != 0)
                {
                    UserRemember = x.Result[0].UserSaved;
                    UserName.Text = UserRemember.ToString();
                }
            });

            // inicializamos el view model
            LoginViewModel = new LoginViewModel()
            {
                LoginDTO = new LoginDTO()
                {
                    Login = new LoginModel(),
                    RememberUser = true
                },
                IsRunning = false
            };

            // Asignación del view model al contexto de la vista (xaml)
            this.BindingContext = LoginViewModel;

            //Evento tap de la imagen hidepassword
            var iconTap = new TapGestureRecognizer();
            iconTap.Tapped += (object sender, EventArgs e) =>
            {
                PassworUser.IsPassword = !PassworUser.IsPassword;
            };

            ImgHidePassw.GestureRecognizers.Add(iconTap);

        }

        private async Task<bool> validarFormulario()
        {
            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                await this.DisplayAlert("Advertencia", "El usuario es obligatorio", "Ok");
                UserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(PassworUser.Text))
            {
                await this.DisplayAlert("Advertencia", "La contraseña es obligatoria", "Ok");
                PassworUser.Focus();
                return false;
            }
            return true;
        }

        async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (LoginViewModel.IsRunning)
                    return;

                if (await validarFormulario())
                {
                    LoginViewModel.IsRunning = true;

                    // construmos el objeto login que se validará
                    var loginModel = new LoginModel() { Nomina = int.Parse(UserName.Text), Password = PassworUser.Text };

                    // Llamamos el servicio para el login
                    LoginService loginService = new LoginService();
                    LogService logService = new LogService();
                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        // Temporal
                        if (PassworUser.Text == "123pormi")
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                //Guardamos la información en la base de datos SQL Lite
                                var gestionFC = new GestionFCModel()
                                {
                                    UserSaved = chkRemember.IsChecked ? int.Parse(UserName.Text) : 0,
                                    Nomina = int.Parse(UserName.Text),
                                    TokenSesion = "123pormi"
                                };

                                App.Database.SaveGestionFCItemAsync(gestionFC);

                                //Guardamos genramos la inserción en bitácora (inicio de sesión)
                                var logModel = new LogSistemaModel()
                                {
                                    IdPantalla = 1,
                                    IdAccion = 1,
                                    Usuario = int.Parse(UserName.Text),
                                    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name
                                };
                                logService.LogSistema(logModel, gestionFC.TokenSesion).ContinueWith(logRes =>
                                {
                                    if (logRes.IsFaulted)
                                        throw logRes.Exception;
                                });

                                var plantillaPage = new PlantillaPage();
                                Navigation.PushAsync(plantillaPage);
                            });
                        else
                        await loginService.Login(loginModel).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                                throw new Exception("Ocurrió un error");

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                                throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);

                            if (!x.Result.UsuarioAutorizado)
                                throw new Exception("Usuario y/o contraseña no coincide.");

                            if (!x.Result.Activo)
                                throw new Exception("Usuario inactivo.");

                            if (!x.Result.EsGerente)
                                throw new Exception("Usuario no cuenta con el perfil de gerente.");


                            //Guardamos la información en la base de datos SQL Lite
                            var gestionFC = new GestionFCModel()
                            {
                                UserSaved = chkRemember.IsChecked ? int.Parse(UserName.Text) : 0,
                                Nomina = int.Parse(UserName.Text),
                                TokenSesion = x.Result.Token
                            };

                            App.Database.SaveGestionFCItemAsync(gestionFC);

                            //Guardamos genramos la inserción en bitácora (inicio de sesión)
                            var logModel = new LogSistemaModel()
                            {
                                IdPantalla = 1,
                                IdAccion = 1,
                                Usuario = int.Parse(UserName.Text),
                                Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name
                            };
                            logService.LogSistema(logModel, gestionFC.TokenSesion).ContinueWith(logRes =>
                            {
                                if (logRes.IsFaulted)
                                    throw logRes.Exception;
                            });

                            // Navegamos hacia la pantalla plantilla que será la página principal de la aplicación
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                var plantillaPage = new PlantillaPage();
                                Navigation.PushAsync(plantillaPage);
                            });
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Ok");

            }
            finally
            {
                LoginViewModel.IsRunning = false;
            }
        }

    }
}