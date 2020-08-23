using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GestionFC.Models.Alertas;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class AlertaService
    {
        private readonly HttpClient _client;

        public AlertaService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<PlantillaImproductivaResponseModel> GetAlertaPlantillaImproductiva(int nomina,string token, int nominaAP = 0)
        {
            var plantillaImproductivaResponse = new PlantillaImproductivaResponseModel();
            try
            {
                var url = $"{App.BaseUrlApi}api/Alerta/GetAlertasPlantillaImproductividad/{nomina}";
                if (nominaAP != 0)
                {
                    url = string.Concat(url, $"/{nominaAP}");
                }

                var uri = new Uri(url);

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                plantillaImproductivaResponse = JsonConvert.DeserializeObject<PlantillaImproductivaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                plantillaImproductivaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return plantillaImproductivaResponse;
        }

        public async Task<PlantillaRecuperacionResponseModel> GetAlertaPlantillaRecuperacion(int nomina, string token)
        {
            var plantillaImproductivaResponse = new PlantillaRecuperacionResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetAlertasPlantillaRecuperacion/{nomina}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                plantillaImproductivaResponse = JsonConvert.DeserializeObject<PlantillaRecuperacionResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                plantillaImproductivaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return plantillaImproductivaResponse;
        }

        public async Task<PlantillaInvestigacionResponseModel> GetAlertaPlantillaInvestigacion(int nomina, string token)
        {
            var plantillaImproductivaResponse = new PlantillaInvestigacionResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetAlertasPlantillaInvestigacion/{nomina}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                plantillaImproductivaResponse = JsonConvert.DeserializeObject<PlantillaInvestigacionResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                plantillaImproductivaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return plantillaImproductivaResponse;
        }

        public async Task<FoliosPendientesSVResponseModel> GetAlertaPlantillaSinSaldoVirtual(int nomina, string token)
        {
            var plantillaImproductivaResponse = new FoliosPendientesSVResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetAlertasPlantillaSinSaldoVirtual/{nomina}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                plantillaImproductivaResponse = JsonConvert.DeserializeObject<FoliosPendientesSVResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                plantillaImproductivaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return plantillaImproductivaResponse;
        }

        public async Task<FoliosPendientesSVResponseModel> GetAlertaPlantillaSeguimientoSinSaldoVirtual(int nomina,int idalerta, string token)
        {
            var plantillaImproductivaResponse = new FoliosPendientesSVResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetAlertasPlantillaSeguimientoSinSaldoVirtual/{nomina}/{idalerta}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                plantillaImproductivaResponse = JsonConvert.DeserializeObject<FoliosPendientesSVResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                plantillaImproductivaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return plantillaImproductivaResponse;
        }

        public async Task<FoliosRecuperacionResponseModel> GetFoliosRecuperacion(int nomina, string token)
        {
            var getFoliosRecuperacionResponse = new FoliosRecuperacionResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetFoliosRecuperacion/{nomina}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                getFoliosRecuperacionResponse = JsonConvert.DeserializeObject<FoliosRecuperacionResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                getFoliosRecuperacionResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return getFoliosRecuperacionResponse;
        }

        public async Task<AlertaRecuperacionResponseModel> GetDetalleFolioRecuperacion(int nomina, int pantallaId, string token)
        {
            var getDetalleFolioRecuperacionResponse = new AlertaRecuperacionResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Alerta/GetDetalleFolioRecuperacion/{nomina}/{pantallaId}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                getDetalleFolioRecuperacionResponse = JsonConvert.DeserializeObject<AlertaRecuperacionResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                getDetalleFolioRecuperacionResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return getDetalleFolioRecuperacionResponse;
        }
    }
}
