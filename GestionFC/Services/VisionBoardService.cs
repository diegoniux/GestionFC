using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GestionFC.Models.Ranking;
using GestionFC.Models.Share;
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
                var uri = new Uri($"{App.BaseUrlApi}api/VisionBoard/GetMetaPlantilla/{nomina}");

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
                var uri = new Uri($"{App.BaseUrlApi}api/VisionBoard/GetMetaPlantillaIndividual/{nomina}");

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

        public async Task<MetaPlantillaResponseModel> RegistrarMetaPlantilla(MetaPlantillaRequestModel metaPlantillaDia, string accessToken)
        {
            var MetaPlantillaResponse = new MetaPlantillaResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/VisionBoard/RegistrarMetaPlantilla");
                var json = JsonConvert.SerializeObject(metaPlantillaDia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                MetaPlantillaResponse = JsonConvert.DeserializeObject<MetaPlantillaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                MetaPlantillaResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
            }
            return MetaPlantillaResponse;
        }

        public async Task<MetaPlantillaIndividualResponseModel> RegistrarMetaPlantillaIndividual(MetaPlantillaIndividualRequestModel metaPlantillaIndividual, string accessToken)
        {
            var MetaPlantillaIndividualResponse = new MetaPlantillaIndividualResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/VisionBoard/RegistrarMetaPlantillaIndividual");
                var json = JsonConvert.SerializeObject(metaPlantillaIndividual);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                MetaPlantillaIndividualResponse = JsonConvert.DeserializeObject<MetaPlantillaIndividualResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                MetaPlantillaIndividualResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
            }
            return MetaPlantillaIndividualResponse;
        }

        public async Task<MetaPlantillaFoliosResponseModel> RegistrarMetaPlantillaFolios(MetaPlantillaFoliosRequestModel metaPlantillaFolios, string accessToken)
        {
            var MetaPlantillaFoliosResponse = new MetaPlantillaFoliosResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/VisionBoard/RegistrarMetaPlantillaFolios");
                var json = JsonConvert.SerializeObject(metaPlantillaFolios);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                MetaPlantillaFoliosResponse = JsonConvert.DeserializeObject<MetaPlantillaFoliosResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                MetaPlantillaFoliosResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
            }
            return MetaPlantillaFoliosResponse;
        }

    }
}
