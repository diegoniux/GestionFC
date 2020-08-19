using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GestionFC.ViewModels.Modals;
using Service = GestionFC.Services;
using Acr.UserDialogs;

namespace GestionFC.Views.Modals
{
    public partial class AlertasRecuperacionModalView : ContentPage
    {
        public AlertasRecuperacionModalViewModel ViewModel { get; set; }
        public int nomina { get; set; }
        public string token { get; set; }
        public bool SesionExpired { get; set; }
        public bool IsBusy { get; set; }

        public AlertasRecuperacionModalView(int nominaAP)
        {
            InitializeComponent();
            LoadPage(nominaAP);
            ViewModel = new AlertasRecuperacionModalViewModel();
            ViewModel.NominaAP = nominaAP;
            BindingContext = ViewModel;
        }

        public async void LoadPage(int nominaAP)
        {
            Service.AlertaService alertaService = new Service.AlertaService();
            try
            {
                nomina = App.Nomina;
                token = App.Token;

                if (nomina > 0)
                {
                    using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                    {
                        await alertaService.GetFoliosRecuperacion(nominaAP, token).ContinueWith(x =>
                        {
                            if (x.IsFaulted)
                            {
                                throw x.Exception;
                            }

                            if (!x.Result.ResultadoEjecucion.EjecucionCorrecta)
                            {

                                // Verificamos si la sesión expiró (token)
                                if (x.Result.ResultadoEjecucion.ErrorMessage.Contains("401"))
                                {
                                    SesionExpired = true;
                                    throw new Exception(x.Result.ResultadoEjecucion.FriendlyMessage);
                                }
                            }
                            // Cargar datos para el binding de información.
                            if (x.Result.ListadoFolios != null)
                            {
                                ViewModel.Folios = x.Result.ListadoFolios;
                            }
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Si la sesión expiró enviamos mensaje 
                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
                    return;
                }

                //var logError = new Models.Log.LogErrorModel()
                //{
                //    IdPantalla = 4,
                //    Usuario = nomina,
                //    Error = (ex.TargetSite == null ? "" : ex.TargetSite.Name + ". ") + ex.Message,
                //    Dispositivo = DeviceInfo.Platform + DeviceInfo.Model + DeviceInfo.Name

                //};
                //await _master.logService.LogError(logError, "").ContinueWith(logRes =>
                //{
                //    if (logRes.IsFaulted)
                //        DisplayAlert("Error", logRes.Exception.Message, "Ok");
                //});
                await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void OnFolio_Tapped(object sender, EventArgs e)
        {
            int registroTraspasoId = Convert.ToInt32(((Grid)sender).ClassId);
            await DisplayAlert("Success", registroTraspasoId.ToString(), "Ok");
        }
        async void CerrarSesion()
        {
            await Navigation.PopModalAsync();
        }
    }
}
