using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Productividad
{
    public class ProductividadSemanalResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("resultSemanas")]
        public ResultSemanasModel ResultSemanas { get; set; }

        [JsonProperty("resultDatos")]
        public List<ProduccionSemanalModel> ResultDatos { get; set; }

        [JsonProperty("resultTotal")]
        public ResultTotalSemanalModel ResultTotal { get; set; }

        [JsonProperty("resultSemanasTotal")]
        public SemanasTotal ResultSemanasTotal { get; set; }
    }
}
