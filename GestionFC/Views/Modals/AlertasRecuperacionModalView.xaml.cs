using System;
using Xamarin.Forms;
using GestionFC.ViewModels.Modals;
using Service = GestionFC.Services;
using Acr.UserDialogs;
using System.IO;
using System.Threading.Tasks;

namespace GestionFC.Views.Modals
{
    public partial class AlertasRecuperacionModalView : ContentPage
    {
        public AlertasRecuperacionModalViewModel ViewModel { get; set; }
        public int nomina { get; set; }
        public string token { get; set; }
        public bool SesionExpired { get; set; }
        //public bool IsBusy { get; set; }
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
            try
            {
                await BorrarArchivosTemp();

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
                                ViewModel.Documentos = x.Result.Documentos;
                                foreach (var item in ViewModel.Documentos)
                                {
                                    string[] base64 = item.Mascara.Split(',');

                                    // Determinamos el tipo de archivo
                                    ViewModel.Documentos[i].IsPdf = false;
                                    ViewModel.Documentos[i].IsImage = false;
                                    ViewModel.Documentos[i].IsVideo = false;

                                    string typeFile = base64[0];
                                    if (typeFile.Contains("pdf"))
                                    {
                                        ViewModel.Documentos[i].IsPdf = true;

                                        string[] source = x.Result.Documentos[i].Mascara.Split(',');
                                        string Base64String = source[1];
                                        byte[] Base64Stream = Convert.FromBase64String(Base64String);

                                        
                                        var folder = Path.GetTempPath();
                                        string fileName = Path.Combine(folder, "temp.pdf");

                                        if (File.Exists(fileName))
                                        {
                                            File.Delete(fileName);
                                        }
                                        File.WriteAllBytes(fileName, Base64Stream);

                                        ViewModel.Documentos[i].Mascara = fileName;

                                    }
                                    else if (typeFile.Contains("jpg"))
                                    {
                                        ViewModel.Documentos[i].IsImage = true;
                                        string[] source = x.Result.Documentos[i].Mascara.Split(',');
                                        string Base64String = source[1];
                                        byte[] Base64Stream = Convert.FromBase64String(Base64String);
                                        ViewModel.Documentos[i].StreamImageSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                                    }
                                    else
                                    {
                                        ViewModel.Documentos[i].IsVideo = true;

                                        string[] source = x.Result.Documentos[i].Mascara.Split(',');
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
                                        ViewModel.Documentos[i].Mascara = fileName;
                                    }
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
            
        }

        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await BorrarArchivosTemp();
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

    }
}
