using System;
using GestionFC.Models.Share;

namespace GestionFC.Models.DetalleEspecialista
{
    public class DetalleFolioResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleFoliosModel DetalleFolios { get; set; }
        public DetalleEtapaModel DetalleEtapas { get; set; }
    }
}
