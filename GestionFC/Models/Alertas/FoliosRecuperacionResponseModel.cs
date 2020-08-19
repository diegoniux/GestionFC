using System;
using System.Collections.Generic;

namespace GestionFC.Models.Alertas
{
    public class FoliosRecuperacionResponseModel
    {
        public Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Share.FolioSolicitud> ListadoFolios { get; set; }

        public FoliosRecuperacionResponseModel()
        {
            this.ResultadoEjecucion = new Share.ResultadoEjecucion();
            this.ListadoFolios = new List<Share.FolioSolicitud>();
        }
    }
}
