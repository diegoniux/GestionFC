using System;
using System.Net.Http;
using System.Threading.Tasks;
using GestionFC.Models.DetalleEspecialista;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class DetalleEspecialistaService
    {
        private readonly HttpClient _client;

        public DetalleEspecialistaService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<DetalleEspecialistaResponseModel> GetDetalleEspecialista(int nominaAP, int nomina, string accessToken)
        {
            var detalleResponse = new DetalleEspecialistaResponseModel();

            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/DetalleEspecialista/GetDetalleEspecialista/{nominaAP}/{nomina}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                detalleResponse = JsonConvert.DeserializeObject<DetalleEspecialistaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                detalleResponse.ResultadoEjecucion  = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return detalleResponse;
        }

        public async Task<DetalleFolioResponseModel> GetDetalleFolio(string folio, string accessToken)
        {
            var detalleResponse = new DetalleFolioResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/DetalleEspecialista/GetDetalleFolio/{folio}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                detalleResponse = JsonConvert.DeserializeObject<DetalleFolioResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                detalleResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return detalleResponse;
        }

    }
}
