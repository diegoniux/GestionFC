using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestionFC.Models.Share;
using Newtonsoft.Json;
using Models = GestionFC.Models;

namespace GestionFC.Services
{
    public class HeaderService
    {
        private readonly HttpClient _client;

        public HeaderService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<HeaderResponseModel> GetHeader(int nomina, string accessToken) {
            var headerResponse = new HeaderResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Header/{nomina}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                headerResponse = JsonConvert.DeserializeObject<HeaderResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                headerResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
            }
            return headerResponse;
        }
    }
}
