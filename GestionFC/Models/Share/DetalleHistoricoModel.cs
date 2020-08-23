using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class DetalleHistoricoModel
    {
        [JsonProperty("tramites")]
        public string Tramites { get; set; }

        [JsonProperty("semana1")]
        public string Semana1 { get; set; }

        [JsonProperty("semana2")]
        public string Semana2 { get; set; }

        [JsonProperty("semana3")]
        public string Semana3 { get; set; }
    }
}
