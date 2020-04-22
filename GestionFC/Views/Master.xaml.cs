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

        public Master()
        {
            InitializeComponent();
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

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }
        }

        void OnTapMenuHamburguesaBar_Tapped(System.Object sender, System.EventArgs e)
        {
            App.MasterDetail.IsPresented = !App.MasterDetail.IsPresented;
        }
    }
}