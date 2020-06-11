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
    public partial class DetalleFolioPage : ContentPage
    {
        public ViewModels.DetalleFolioPage.DetalleFolioPageViewModel ViewModel { get; set; }
        public DetalleFolioPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //ViewModel = new ViewModels.DetalleFolioPage.DetalleFolioPageViewModel();

            //BindingContext = ViewModel;
        }
    }
}