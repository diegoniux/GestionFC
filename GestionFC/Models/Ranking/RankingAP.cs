using System;

namespace GestionFC.Models.Ranking
{
    public class RankingAP
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }
        public string Posicion { get; set; }
        public string Saldo { get; set; }
        public string TipoSaldo { get; set; }
        public int NumTraspaso { get; set; }
        public string ImgPosicionSemAnt { get; set; }
        public string ColorPosicion { get; set; }
        public RankEstrellas Estrellas { get; set; }
    }
}
