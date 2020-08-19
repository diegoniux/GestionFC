using System;

namespace GestionFC.Models.Share
{
    public class RankingGte
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
        public string ColorTextoSaldo { get; set; }
        public RankSemanal Estrellas { get; set; }
    }
}
