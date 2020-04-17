using GestionFC.Models.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage

    {
        public Master()
        {
            InitializeComponent();
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Cerrar Sesión",
                IconSource = "exit.png",
                TargetType = typeof(LoginPage),
                IsClosed = true
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Plantilla",
                IconSource = "exit.png",
                TargetType = typeof(PlantillaPage),
                IsClosed = true
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Productividad",
                IconSource = "exit.png",
                TargetType = typeof(ProductividadPage),
                IsClosed = true
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

                //if (item.IsClosed)
                //{
                //    App.MasterDetail.Detail.Navigation.PopAsync();
                //}
                //else
                //{
                //    App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                //    listView.SelectedItem = null;
                //    App.MasterDetail.IsPresented = false;
                //}

                
            }
        }
    }
}