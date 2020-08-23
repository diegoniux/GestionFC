using System;
using System.Collections.Generic;
using GestionFC.Models.Share;

namespace GestionFC.Models.DetalleEspecialista
{
    public class DetalleFolioResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleFoliosModel DetalleFolios { get; set; }
        public List<DetalleEtapaModel> DetalleEtapas { get; set; }
    }
}
