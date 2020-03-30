using GestionFC.Models.Login;
using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionFC.Services
{
    public class LoginService
    {
        private readonly HttpClient _client;

        public LoginService()
        {
            _client = new HttpClient();
        }

        public async Task<LoginResponseModel> Login(LoginModel login)
        {
            var loginResponse = new LoginResponseModel();
            try
            {
                var uri = new Uri(App.BaseUrlApi + "api/Login");
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();
                
                string jsonResult = await response.Content.ReadAsStringAsync();
                loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonResult);
                
                return loginResponse;
            }
            catch (Exception ex)
            {
                loginResponse.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = false,
                    FriendlyMessage = "Ocurrió un error",
                    ErrorMessage = ex.Message
                };
                return loginResponse;
            }
            
        }

    }
}
