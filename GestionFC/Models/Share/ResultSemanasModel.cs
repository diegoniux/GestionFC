using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public partial class ResultSemanasModel
    {
        [JsonProperty("semana1")]
        public string Semana1 { get; set; }

        [JsonProperty("semana2")]
        public string Semana2 { get; set; }

        [JsonProperty("semana3")]
        public string Semana3 { get; set; }

        [JsonProperty("semana4")]
        public string Semana4 { get; set; }
    }
}
