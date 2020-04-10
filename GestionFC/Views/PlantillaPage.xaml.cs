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

        public PlantillaPage()
        {
            InitializeComponent();
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

            //this.BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void loadPage()
        {
            Service.HeaderService headerService = new Service.HeaderService();
            Service.GridPromotoresService gridPromotoresService = new Service.GridPromotoresService();
            int nomina = 0;
            try
            {
                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    headerService.GetHeader(nomina).ContinueWith(x =>
                    {

                    });

                    gridPromotoresService.GetGridPromotores(nomina).ContinueWith(x =>
                    {
                        this.BindingContext = x.Result.Promotores;
                    });

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}