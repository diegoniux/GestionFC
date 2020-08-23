using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class DetalleHistoricoSemanasModel
    {
        [JsonProperty("fechaSemana1")]
        public string FechaSemana1 { get; set; }

        [JsonProperty("fechaSemana2")]
        public string FechaSemana2 { get; set; }

        [JsonProperty("fechaSemana3")]
        public string FechaSemana3 { get; set; }
    }
}
