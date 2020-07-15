using GestionFC.Models.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Service = GestionFC.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using GestionFC.Models.Log;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage

    {
        public ViewModels.Master.MasterViewModel ViewModel { get; set; }
        private int _nomina { get; set; }
        private string _token { get; set; }
        public Service.LogService logService { get; set; }

        public Master()
        {
            InitializeComponent();
            version.Text = $"Versión: {VersionTracking.CurrentVersion}";
            ViewModel = new ViewModels.Master.MasterViewModel();
            logService = new Service.LogService();
            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Productividad",
                IconSource = "icon_productividad.png",
                TargetType = typeof(PlantillaPage),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Pizarrón Digital",
                IconSource = "icon_pizarron_digital.png",
                TargetType = typeof(ProductividadPage),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Alarmas",
                IconSource = "icon_notification.png",
                TargetType = typeof(Alertas),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Objetivos",
                IconSource = "icon_target.png",
                TargetType = typeof(VisionBoardPage),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Ranking",
                IconSource = "icon_ranking.png",
                TargetType = typeof(RankingPage),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Cerrar Sesión",
                IconSource = "icon_singout.png",
                TargetType = typeof(LoginPage),
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Ranking Semanal",
                IconSource = "icon_ranking.png",
                TargetType = typeof(RankingAgentesPage),
            });

            listView.ItemsSource = masterPageItems;
            //loadPage();

        }

        public async void loadPage(int nomina, string nombreCompleto, string puesto, string foto, string token)
        {
            try
            {
                _nomina = nomina;
                _token = token;
                //Cargar datros para el binding de información con el header
                ViewModel.NombreGerenteMaster = nombreCompleto;
                ViewModel.Puesto = puesto;
                ViewModel.Foto = foto;

                Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContext = ViewModel;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("MasterPage Error", ex.Message, "Ok");
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                if (item.Title == "Cerrar Sesión")
                {
                    if (!await DisplayAlert("Alerta", "¿Está seguro que desea cerrar sesión?", "Ok", "Cancelar"))
                    {
                        listView.SelectedItem = null;
                        return;
                    }
                    await cerrarSesion(item);
                }
                else
                {
                    App.MasterDetail.IsPresented = false;
                    await App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                    listView.SelectedItem = null;
                }
            }
        }


        public async Task cerrarSesionError() {
            await cerrarSesion(null);
        }

        private async Task cerrarSesion(MasterPageItem item) {
            int nomina = _nomina;
            //Guardamos genramos la inserción en bitácora
            Device.BeginInvokeOnMainThread(() =>
            {
                ViewModel.getLocation().ContinueWith(loc => {
                    //Guardamos genramos la inserción en bitácora (Cierre Sesión)
                    var logModel = new LogSistemaModel()
                    {
                        IdPantalla = 1,
                        IdAccion = 3,
                        Usuario = nomina,
                        Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name,
                        Geolocalizacion = loc.Result
                    };
                    logService.LogSistema(logModel, _token).ContinueWith(logRes =>
                    {
                        if (logRes.IsFaulted)
                            throw logRes.Exception;
                    });
                    
                });
            });
            App.MasterDetail.IsPresented = false;
            loadPage(0, string.Empty, string.Empty, "capi_circulo.png", _token);

            if (item != null)
            {
                await App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
            }
        }

        void OnTapMenuHamburguesaBar_Tapped(System.Object sender, System.EventArgs e)
        {
            App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
        }
    }
}