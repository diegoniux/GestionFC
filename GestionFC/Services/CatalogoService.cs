using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestionFC.Models.Catalogo;
using Newtonsoft.Json;

namespace GestionFC.Services
{
    public class CatalogoService
    {
        private readonly HttpClient _client;
        public CatalogoService()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            this._client = new HttpClient(httpClientHandler);
        }

        public async Task<CatalogoResponseModel> GetCatalogo(CatalogoModel catalogoModel)
        {
            var catalogoResponse = new CatalogoResponseModel();
            try
            {
                var uri = new Uri($"{App.BaseUrlApi}api/Catalogo/{catalogoModel.Clave}");

                HttpResponseMessage response = null;
                response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();
                catalogoResponse = JsonConvert.DeserializeObject<CatalogoResponseModel>(jsonResult);
            }
            catch (Exception ex)
            {
                catalogoResponse.ResultadoEjecucion = new Models.Share.ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
            }
            return catalogoResponse;
        }

    }
}
