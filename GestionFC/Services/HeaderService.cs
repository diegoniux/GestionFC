using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Models = GestionFC.Models;

namespace GestionFC.Services
{
    public class HeaderService
    {
        private readonly HttpClient _client;

        public HeaderService()
        {
            this._client = new HttpClient();
        }

        public async Task<Models.PlantillaPage.HeaderResponseModel> GetHeader(int nomina) {
            var headerResponse = new Models.PlantillaPage.HeaderResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/GetHeader/" + nomina.ToString());
                var content = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                headerResponse = JsonConvert.DeserializeObject<Models.PlantillaPage.HeaderResponseModel>(jsonResult);
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
