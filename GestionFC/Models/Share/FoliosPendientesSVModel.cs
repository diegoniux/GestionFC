using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FoliosPendientesSVModel
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("saldoVirtual")]
        public string SaldoVirtual { get; set; }

        [JsonProperty("tipoSolicitud")]
        public string TipoSolicitud { get; set; }

        [JsonProperty("fechaFirma")]
        public string FechaFirma { get; set; }

        [JsonProperty("tieneSV")]
        public bool TieneSV { get; set; }
    }
}
