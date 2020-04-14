using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Models = GestionFC.Models;

namespace GestionFC.Services
{
    public class GridPromotoresService
    {
        private readonly HttpClient _client;

        public GridPromotoresService()
        {
            this._client = new HttpClient();
        }

        public async Task<Models.PlantillaPage.GridPromotoresResponseModel> GetGridPromotores(int nomina)
        {
            var gridPromotoresResponse = new Models.PlantillaPage.GridPromotoresResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/GetGridPromotores/" + nomina.ToString());
                var content = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                gridPromotoresResponse = JsonConvert.DeserializeObject<Models.PlantillaPage.GridPromotoresResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                gridPromotoresResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return gridPromotoresResponse;
        }
    }
}
