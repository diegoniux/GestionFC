using GestionFC.Models.Share;
using System;
using System.Collections.Generic;

namespace GestionFC.Models.Plantilla
{
    public class PromotoresResponseModel
    {
        public Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Progreso> Promotores { get; set; }
    }
}
