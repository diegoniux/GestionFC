using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GestionFC.Models.Ranking;
using GestionFC.Models.VisionBoard;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class VisionBoardService
    {
        private readonly HttpClient _client;

        public VisionBoardService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<GetMetaPlantillaResponseModel> GetMetaPlantilla(int nomina, string accessToken)
        {
            var getMetaPlantillaResponse = new GetMetaPlantillaResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/GetMetaPlantilla/{nomina}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                getMetaPlantillaResponse = JsonConvert.DeserializeObject<GetMetaPlantillaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                getMetaPlantillaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return getMetaPlantillaResponse;
        }

        public async Task<GetMetaPlantillaIndividualResponseModel> GetMetaPlantillaIndividual(int nomina, string accessToken)
        {
            var getMetaPlantillaIndividualResponse = new GetMetaPlantillaIndividualResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/GetMetaPlantillaIndividual/{nomina}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                getMetaPlantillaIndividualResponse = JsonConvert.DeserializeObject<GetMetaPlantillaIndividualResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                getMetaPlantillaIndividualResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return getMetaPlantillaIndividualResponse;
        }
    }
}
