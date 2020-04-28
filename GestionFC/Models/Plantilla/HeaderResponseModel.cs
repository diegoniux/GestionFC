using System;
namespace GestionFC.Models.Plantilla
{
    public class HeaderResponseModel
    {
        public Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Plantilla { get; set; }
        public int APsMetaAlcanzada { get; set; }
        public string Perfil { get; set; }
        public Progreso Progreso { get; set; }
    }
}
