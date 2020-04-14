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
            //LoadPage();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.PlantillaService gridPromotoresService = new Service.PlantillaService();
            int nomina = 0;
            try
            {
                string token = string.Empty;
                await App.Database.GetGestionFCItemAsync().ContinueWith(x => {
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
                        //Cargar datros para el binding de información con el header

                    });

                    await gridPromotoresService.GetGridPromotores(nomina, token).ContinueWith(x =>
                      {
                          ViewModel.Agentes = x.Result.Promotores;
                          if(ViewModel.Agentes?.Count > 0)
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