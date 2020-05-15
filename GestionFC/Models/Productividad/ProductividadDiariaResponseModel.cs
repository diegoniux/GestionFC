using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestionFC.Models.Productividad
{
    public class ProductividadDiariaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("resultFechas")]
        public ResultFechasModel ResultFechas { get; set; }

        [JsonProperty("resultDatos")]
        public List<ProduccionDiariaModel> ResultDatos { get; set; }

        [JsonProperty("resultAnioSemana")]
        public ResultAnioSemanaModel ResultAnioSemana { get; set; }

    }
}
