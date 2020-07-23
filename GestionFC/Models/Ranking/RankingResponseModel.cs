using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GestionFC.Models.Ranking
{
    public class RankingResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        [JsonProperty("topGerentes")]
        public List<RankingGte> TopGerentes { get; set; }
        [JsonProperty("gerentes")]
        public List<RankingGte> Gerentes { get; set; }
        [JsonProperty("posicionDireccion")]
        public int PosicionDireccion { get; set; }
        [JsonProperty("imgPosicionSemAntDireccion")]
        public string ImgPosicionSemAntDireccion { get; set; }
        [JsonProperty("posicionNacional")]
        public int PosicionNacional { get; set; }
        [JsonProperty("imgPosicionSemAntNacional")]
        public string ImgPosicionSemAntNacional { get; set; }
    }
}
