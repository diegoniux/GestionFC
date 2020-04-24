using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ResultAnioSemanaModel
    {
        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("semanaAnio")]
        public int SemanaAnio { get; set; }

        [JsonProperty("esActual")]
        public bool EsActual { get; set; }
    }
}
