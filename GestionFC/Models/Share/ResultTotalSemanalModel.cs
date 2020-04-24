using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ResultTotalSemanalModel
    {
        [JsonProperty("saldoVirtualTotal")]
        public int SaldoVirtualTotal { get; set; }

        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("tetrasemanaAnio")]
        public int TetrasemanaAnio { get; set; }

        [JsonProperty("esActual")]
        public bool EsActual { get; set; }
    }
}
