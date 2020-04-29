using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GestionFC.Models.Plantilla;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class PlantillaService
    {
        private readonly HttpClient _client;

        public PlantillaService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<PromotoresResponseModel> GetGridPromotores(int nomina, string token)
        {
            var promotoresResponse = new PromotoresResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Plantilla/{nomina}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                promotoresResponse = JsonConvert.DeserializeObject<PromotoresResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                promotoresResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return promotoresResponse;
        }
    }
}
