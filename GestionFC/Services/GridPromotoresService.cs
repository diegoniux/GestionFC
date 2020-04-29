﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestionFC.Models.Plantilla;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class GridPromotoresService
    {
        private readonly HttpClient _client;

        public GridPromotoresService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<GridPromotoresResponseModel> GetGridPromotores(int nomina)
        {
            var gridPromotoresResponse = new GridPromotoresResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/GetGridPromotores/" + nomina.ToString());
                var content = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                gridPromotoresResponse = JsonConvert.DeserializeObject<GridPromotoresResponseModel>(jsonResult);
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
