using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestionFC.Models.Ranking
{
    public class RankingAgentesResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        [JsonProperty("topEspecialistas")]
        public List<RankingEspecialista> TopEspecialistas { get; set; }
        [JsonProperty("especialistas")]
        public List<RankingEspecialista> Especialistas { get; set; }
        [JsonProperty("dias")]
        public string Dias { get; set; }
        [JsonProperty("horas")]
        public string Horas { get; set; }
    }
}
