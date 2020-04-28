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

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage

    {
        public ViewModels.Master.MasterViewModel ViewModel { get; set; }

        public Master()
        {
            InitializeComponent();
            ViewModel = new ViewModels.Master.MasterViewModel();
            var masterPageItems = new List<MasterPageItem>();
            //masterPageItems.Add(new MasterPageItem
            //{
            //    Title = "Plantilla",
            //    IconSource = "exit.png",
            //    TargetType = typeof(PlantillaPage),
            //});
            //masterPageItems.Add(new MasterPageItem
            //{
            //    Title = "Productividad",
            //    IconSource = "exit.png",
            //    TargetType = typeof(ProductividadPage),
            //});

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Cerrar Sesión",
                IconSource = "exit.png",
                TargetType = typeof(LoginPage),
            });

            listView.ItemsSource = masterPageItems;
            //loadPage();

        }

        public async void loadPage(int nomina, string nombreCompleto, string puesto)
        {
            try
            {
                //Cargar datros para el binding de información con el header
                ViewModel.NombreGerenteMaster = nombreCompleto;
                ViewModel.Puesto = puesto;
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
                }
                App.MasterDetail.IsPresented = false;
                loadPage(0, string.Empty, string.Empty);
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