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
    public partial class PlantillaPage : ContentPage
    {
        public PlantillaPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}