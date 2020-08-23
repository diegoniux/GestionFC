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
        public int registroTraspasoId { get; set; }
        public int pantallaId { get; set; }

        public AlertasRecuperacionModalView(int nominaAP, Models.Alertas.PlantillaRecuperacionModel especialista)
        {
            InitializeComponent();
            ViewModel = new AlertasRecuperacionModalViewModel();
            ViewModel.NominaAP = nominaAP;
            ViewModel.NombreAP = especialista.NombreAP;
            ViewModel.ApellidosAP = especialista.ApellidosAP;
            ViewModel.FotoAP = especialista.Foto;
            LoadPage(nominaAP);
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

        async void getDocs() {
            Service.AlertaService alertaService = new Service.AlertaService();

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                await alertaService.GetDetalleFolioRecuperacion(registroTraspasoId, pantallaId, token).ContinueWith(x => {
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
                    if (x.Result.Pantallas != null)
                    {
                        ViewModel.Pantallas = x.Result.Pantallas;
                        foreach (var item in ViewModel.Pantallas)
                        {
                            if (item.PantallaId == pantallaId)
                                ViewModel.TituloPantallaDoc = item.PantallaDesc;
                            else if (pantallaId == 0)
                                ViewModel.TituloPantallaDoc = ViewModel.Pantallas[0].PantallaDesc;
                        }
                        if (x.Result.Documentos != null)
                        {
                            ViewModel.Documentos = x.Result.Documentos;
                            int i = 0;
                            foreach (var item in ViewModel.Documentos)
                            {
                                string[] base64 = item.Mascara.Split(',');
                                ViewModel.Documentos[i].archivoDocumento = new System.IO.MemoryStream(Convert.FromBase64String(base64[1]));
                                i++;
                            }
                            //ViewModel.Documentos[0].Mascara = ViewModel.FotoAP;
                            if (x.Result.Preguntas != null)
                            {
                                ViewModel.Preguntas = x.Result.Preguntas;
                            }
                        }
                    }
                });
            }
        }

        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void OnPantalla_Tapped(object sender, EventArgs e)
        {
            pantallaId = Convert.ToInt32(((Grid)sender).ClassId);

            getDocs();
        }

        void OnFolio_Tapped(object sender, EventArgs e)
        {
            registroTraspasoId = Convert.ToInt32(((Grid)sender).ClassId);
            pantallaId = 0;

            getDocs();
        }
        async void CerrarSesion()
        {
            await Navigation.PopModalAsync();
        }
    }
}
