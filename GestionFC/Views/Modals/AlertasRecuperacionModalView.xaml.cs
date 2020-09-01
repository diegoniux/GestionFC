using System;
using Xamarin.Forms;
using GestionFC.ViewModels.Modals;
using Service = GestionFC.Services;
using Acr.UserDialogs;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using GestionFC.Models.Share;
using System.Linq;

namespace GestionFC.Views.Modals
{
    public partial class AlertasRecuperacionModalView : ContentPage
    {
        public AlertasRecuperacionModalViewModel ViewModel { get; set; }
        public int nomina { get; set; }
        public string token { get; set; }
        public bool SesionExpired { get; set; }
        //public bool IsBusy { get; set; }
        public FolioSolicitud FolioActual { get; set; }
        public AlertaRecuperacionPantallas PantallaActual { get; set; }

        public AlertasRecuperacionModalView(int nominaAP, Models.Alertas.PlantillaRecuperacionModel especialista)
        {
            InitializeComponent();
            ViewModel = new AlertasRecuperacionModalViewModel();
            ViewModel.NominaAP = nominaAP;
            ViewModel.NombreAP = especialista.NombreAP;
            ViewModel.ApellidosAP = especialista.ApellidosAP;
            ViewModel.FotoAP = especialista.Foto;
            BindingContext = ViewModel;
            LoadPage(nominaAP);
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
                    Device.BeginInvokeOnMainThread(async() =>
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
                                    FolioActual = ViewModel.Folios.FirstOrDefault();
                                    ViewModel.Folios.Find(folio =>
                                        folio.RegistroTraspasoId == FolioActual.RegistroTraspasoId).BackGroundColor = "#DCD7D7";

                                }
                            });
                            PantallaActual = new AlertaRecuperacionPantallas();
                            await getDocs();
                        }
                    });
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

        async Task<bool> getDocs()
        {
            try
            {
                await BorrarArchivosTemp();

                Service.AlertaService alertaService = new Service.AlertaService();

                using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
                {
                    await alertaService.GetDetalleFolioRecuperacion(int.Parse(FolioActual.RegistroTraspasoId), PantallaActual.PantallaId, token).ContinueWith(x =>
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
                        if (x.Result.Pantallas != null)
                        {
                            ViewModel.Pantallas = x.Result.Pantallas;

                            foreach (var item in ViewModel.Pantallas)
                            {
                                ViewModel.Pantallas.Find(pantalla =>
                                    pantalla.PantallaId == item.PantallaId).BackGroundColor = "#FFFFFF";

                                if (item.PantallaId == PantallaActual.PantallaId)
                                {
                                    ViewModel.Pantallas.Find(pantalla =>
                                    pantalla.PantallaId == PantallaActual.PantallaId).BackGroundColor = "#DCD7D7";
                                    ViewModel.TituloPantallaDoc = item.PantallaDesc;
                                }
                                else if (PantallaActual.PantallaId == 0)
                                {
                                    PantallaActual = ViewModel.Pantallas[0];
                                    ViewModel.TituloPantallaDoc = PantallaActual.PantallaDesc;

                                    ViewModel.Pantallas.Find(pantalla =>
                                    pantalla.PantallaId == PantallaActual.PantallaId).BackGroundColor = "#DCD7D7";
                                }
                                    
                            }

                            if (x.Result.Documentos != null)
                            {

                                var DocsApoyo = new List<Models.Share.AlertaRecuperacionDocumentos>();
                                var DocsPrincipal = new List<Models.Share.AlertaRecuperacionDocumentos>();
                                ViewModel.Documentos = x.Result.Documentos;

                                int i = 0;
                                ViewModel.Documentos = x.Result.Documentos;
                                foreach (var item in ViewModel.Documentos)
                                {
                                    if (item.Mascara == null)
                                    {
                                        item.Mascara = "";
                                    }

                                    string[] base64 = item.Mascara.Split(',');
                                    string typeFile = base64[0];

                                    if (typeFile.Contains("pdf"))
                                    {

                                        item.IsPdf = true;

                                        string[] source = item.Mascara.Split(',');
                                        string Base64String = source[1];
                                        byte[] Base64Stream = Convert.FromBase64String(Base64String);


                                        var folder = Path.GetTempPath();
                                        string fileName = Path.Combine(folder, "temp.pdf");

                                        if (File.Exists(fileName))
                                        {
                                            File.Delete(fileName);
                                        }
                                        File.WriteAllBytes(fileName, Base64Stream);

                                        item.Mascara = fileName;
                                    }
                                    else if (typeFile.Contains("jpg"))
                                    {

                                        item.IsImage = true;
                                        string[] source = item.Mascara.Split(',');
                                        string Base64String = source[1];
                                        byte[] Base64Stream = Convert.FromBase64String(Base64String);
                                        item.StreamImageSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                                    }
                                    else if (typeFile.Contains("mp4"))
                                    {

                                        item.IsVideo = true;

                                        string[] source = item.Mascara.Split(',');
                                        string Base64String = source[1];
                                        byte[] Base64Stream = Convert.FromBase64String(Base64String);

                                        // var directory = "temp";
                                        var folder = Path.GetTempPath();
                                        string fileName = Path.Combine(folder, "temp.mp4");

                                        if (File.Exists(fileName))
                                        {
                                            File.Delete(fileName);
                                        }

                                        File.WriteAllBytes(fileName, Base64Stream);

                                        //ViewModel.Documentos[i].VideoUri = new Uri($"ms-appdata:///{directory}/temp.mp4");
                                        item.Mascara = fileName;
                                    }
                                    else
                                    {
                                        item.isData = true;
                                    }

                                    if (item.EsPrincipal)
                                        DocsPrincipal.Add(item);
                                    else
                                        DocsApoyo.Add(item);

                                    i++;
                                }

                                ViewModel.DocumentosApoyo = DocsApoyo;
                                ViewModel.DocumentosPrincipal = DocsPrincipal;

                                if (x.Result.Preguntas != null)
                                {
                                    ViewModel.Preguntas = x.Result.Preguntas;
                                }
                            }
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                // Si la sesión expiró enviamos mensaje 
                if (SesionExpired)
                {
                    CerrarSesion();
                    IsBusy = false;
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
                return false;
            }

        }

        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await BorrarArchivosTemp();
            await Navigation.PopModalAsync();
        }

        async void CerrarSesion()
        {
            await Navigation.PopModalAsync();
        }

        async Task<bool> BorrarArchivosTemp()
        {
            try
            {
                var folder = Path.GetTempPath();
                string fileName = Path.Combine(folder, "temp.mp4");

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                folder = Path.GetTempPath();
                fileName = Path.Combine(folder, "temp.pdf");

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }

        }

        //async void CollectionViewFolios_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        //{
        //    if (IsBusy)
        //    {
        //        return;
        //    }

        //    if (e.CurrentSelection == null)
        //    {
        //        return;
        //    }

        //    try
        //    {

        //        IsBusy = true;

        //        FolioActual = (e.CurrentSelection.FirstOrDefault() as FolioSolicitud);
        //        PantallaActual = new AlertaRecuperacionPantallas();
        //        await getDocs();
        //        CollectionViewPantallas.SelectedItem = ViewModel.Pantallas.FirstOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //async void CollectionViewPantallas_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        //{
        //    if (IsBusy)
        //    {
        //        return;
        //    }

        //    if (e.CurrentSelection == null)
        //    {
        //        return;
        //    }

        //    try
        //    {

        //        IsBusy = true;

        //        PantallaActual = (e.CurrentSelection.FirstOrDefault() as AlertaRecuperacionPantallas);
        //        await getDocs();

        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        async void TapPantalla_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {

                foreach (var item in ViewModel.Pantallas)
                {
                    ViewModel.Pantallas.Find(pantalla =>
                                    pantalla.PantallaId == item.PantallaId).BackGroundColor = "#FFFFFF";
                }


                IsBusy = true;
                PantallaActual = ViewModel.Pantallas.Find( pantalla =>
                    pantalla.PantallaId == int.Parse((sender as Grid).ClassId));

                ViewModel.Pantallas.Find(pantalla =>
                                    pantalla.PantallaId == PantallaActual.PantallaId).BackGroundColor = "#DCD7D7";

                await getDocs();

            }
            catch (Exception ex)
            {
                await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void TapFolio_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {

                foreach (var item in ViewModel.Folios)
                {
                    ViewModel.Folios.Find(folio =>
                        folio.RegistroTraspasoId == item.RegistroTraspasoId).BackGroundColor = "#FFFFFF";
                }


                IsBusy = true;

                FolioActual = ViewModel.Folios.Find(folio => folio.RegistroTraspasoId == (sender as Grid).ClassId);
                ViewModel.Folios.Find(folio =>
                        folio.RegistroTraspasoId == FolioActual.RegistroTraspasoId).BackGroundColor = "#DCD7D7";
                PantallaActual = new AlertaRecuperacionPantallas();
                await getDocs();

            }
            catch (Exception ex)
            {
                await DisplayAlert("PlantillaPage Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
