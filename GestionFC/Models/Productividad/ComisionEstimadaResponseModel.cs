using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Productividad
{
    public class ComisionEstimadaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("comisionEstimada")]
        public string ComisionEstimada { get; set; }

        [JsonProperty("bonoExcelenciaEstimado")]
        public string BonoExcelenciaEstimado { get; set; }
    }
}
