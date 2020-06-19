using GestionFC.Models.Share;
using System;
using System.Collections.Generic;

namespace GestionFC.Models.Ranking
{
    public class RankingResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<RankingAP> TopGerentes { get; set; }
        public List<RankingAP> Gerentes { get; set; }
        public int PosicionDireccion { get; set; }
        public string ImgPosicionSemAntDireccion { get; set; }
        public int PosicionNacional { get; set; }
        public string ImgPosicionSemAntNacional { get; set; }
    }
}
