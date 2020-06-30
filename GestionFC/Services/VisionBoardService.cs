using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GestionFC.Models.Ranking;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class RankingService
    {
        private readonly HttpClient _client;

        public RankingService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<RankingResponseModel> GetRanking(int nomina, string accessToken)
        {
            var rankingResponse = new RankingResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Ranking/{nomina}");

                HttpResponseMessage response = null;
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                rankingResponse = JsonConvert.DeserializeObject<RankingResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                rankingResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return rankingResponse;
        }
    }
}
