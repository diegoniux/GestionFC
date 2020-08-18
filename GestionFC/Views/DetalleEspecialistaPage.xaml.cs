using System;
using System.Collections.Generic;
using GestionFC.Models.Share;
using Xamarin.Forms;

namespace GestionFC.Views
{
    public partial class DetalleEspecialistaPage : ContentPage
    {
        public List<Progreso> Especialistas { get; set; }
        public Progreso EspecialistaActual { get; set; }

        public DetalleEspecialistaPage(List<Progreso> especialistas, Progreso especialista)
        {
            InitializeComponent();

            // NavigationPage.SetHasNavigationBar(this, false);

            Especialistas = especialistas;
            EspecialistaActual = especialista;

            DisplayAlert("info", Especialistas.ToJson().ToString(), "OK");

        }
    }
}
