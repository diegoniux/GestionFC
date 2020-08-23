using System;
using System.Collections.Generic;
using GestionFC.Models.Share;

namespace GestionFC.Models.DetalleEspecialista
{
    public class DetalleHistoricoResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleHistoricoHeaderModel DetalleHistoricoHeader { get; set; }
        public DetalleHistoricoSemanasModel DetalleHistoricoSemanas { get; set; }
        public List<DetalleHistoricoModel> DetalleHistoricoTramites { get; set; }
        public List<DetalleHistoricoModel> DetalleHistoricoRecuperacion { get; set; }
    }
}
