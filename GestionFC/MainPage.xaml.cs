using GestionFC.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestionFC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new Master();
            Detail = new NavigationPage(new LoginPage());
            //{
            //    BarTextColor = Color.White
            //};

            App.MasterDetail = this;

            // La siguiente línea para que en tabletas o en modo landscape se oculte el menú hamburguesa
            //this.MasterBehavior = MasterBehavior.Popover;
        }
    }
}
