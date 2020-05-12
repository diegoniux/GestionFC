using GestionFC.Models.Productividad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GestionFC.Services
{
    public class ProductividadService
    {
        private readonly HttpClient _client;

        public ProductividadService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<ProductividadDiariaResponseModel> GetProduccionDiaria(int nomina, int anio, int semanaAnio, string token)
        {
            var productividadDiariaResponse = new ProductividadDiariaResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Productividad/GetProductividadDiaria/{nomina}/{anio}/{semanaAnio}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                productividadDiariaResponse = JsonConvert.DeserializeObject<ProductividadDiariaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                productividadDiariaResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return productividadDiariaResponse;
        }

        public async Task<ProductividadSemanalResponseModel> GetProduccionSemanal(int nomina, int anio, int tetrasemanaAnio, string token, DateTime fechaCorte, bool esPosterior)
        {
            var productividadSemanalResponse = new ProductividadSemanalResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Productividad/GetProductividadSemanal/{nomina}/{anio}/{tetrasemanaAnio}/{fechaCorte:yyyy-MM-dd}/{esPosterior}");
                               

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                productividadSemanalResponse = JsonConvert.DeserializeObject<ProductividadSemanalResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                productividadSemanalResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return productividadSemanalResponse;
        }

        public async Task<ComisionEstimadaResponseModel> GetComisionEstimada(int nomina, DateTime fecha, string token)
        {
            var ComisionEstimadaResonse = new ComisionEstimadaResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Productividad/GetComisionEstimada/{nomina}/{fecha.ToString("yyyy-MM-dd")}");

                HttpResponseMessage response = null;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                ComisionEstimadaResonse = JsonConvert.DeserializeObject<ComisionEstimadaResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                ComisionEstimadaResonse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrio un error",
                    ErrorMessage = ex.Message
                };
            }
            return ComisionEstimadaResonse;
        }

    }
}
