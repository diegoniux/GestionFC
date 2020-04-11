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

        public PlantillaPage()
        {
            InitializeComponent();
            ViewModel = new ViewModels.PlantillaPage.PlantillaPageViewModel();
            loadPage();
            //ViewModel.Agentes = new List<Model.AgenteDTO>
            //{
            //    new Model.AgenteDTO()
            //    {
            //        Nomina = 23401,
            //        Nombre = "Josefina",
            //        Apellidos = "Vázquez Lugo",
            //        Avance = 0,
            //        EtiquetaAvance = "0%",
            //        ImagenUrl = "ap_1.png"
            //    }
            //};

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void loadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.GridPromotoresService gridPromotoresService = new Service.GridPromotoresService();
            int nomina = 0;
            try
            {
                string token = string.Empty;
                await App.Database.GetGestionFCItemAsync().ContinueWith(x => {
                    if (!string.IsNullOrEmpty(x.Result[0].TokenSesion))
                    {
                        token = x.Result[0].TokenSesion;
                    }
                });

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await headerService.GetHeader(nomina).ContinueWith(x =>
                    {

                    });

                    await gridPromotoresService.GetGridPromotores(nomina, token).ContinueWith(x =>
                      {
                          ViewModel.Agentes = x.Result.Promotores;
                          if(ViewModel.Agentes.Count > 0)
                          Device.BeginInvokeOnMainThread(() =>
                          {
                              BindingContext = ViewModel;
                          });
                      });

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}