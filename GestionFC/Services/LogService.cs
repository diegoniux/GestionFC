using GestionFC.Models.Log;
using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionFC.Services
{
    public class LogService
    {

        private readonly HttpClient _client;

        public LogService()
        {
            _client = new HttpClient();
        }

        public async Task<LogSistemaResponseModel> LogSistema(LogSistemaModel log, string accessToken)
        {
            var logResponse = new LogSistemaResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/Log/SetLogSistema");
                var json = JsonConvert.SerializeObject(log);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                logResponse = JsonConvert.DeserializeObject<LogSistemaResponseModel>(jsonResult);

                return logResponse;
            }
            catch (Exception ex)
            {
                logResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
                return logResponse;
            }

        }

        public async Task<LogErrorResponseModel> LogError(LogErrorModel log, string accessToken)
        {
            var logResponse = new LogErrorResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/Log/SetLogError");
                var json = JsonConvert.SerializeObject(log);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                logResponse = JsonConvert.DeserializeObject<LogErrorResponseModel>(jsonResult);

                return logResponse;
            }
            catch (Exception ex)
            {
                logResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
                return logResponse;
            }

        }

    }
}
