using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FoliosPendientesSVModel
    {
        [JsonProperty("idAlerta")]
        public int IdAlerta { get; set; }

        [JsonProperty("idTipoAlerta")]
        public int IdTipoAlerta { get; set; }

        [JsonProperty("idEstatusAlerta")]
        public int IdEstatusAlerta { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("saldoVirtual")]
        public string SaldoVirtual { get; set; }

        [JsonProperty("tipoSolicitud")]
        public string TipoSolicitud { get; set; }

        [JsonProperty("fechaFirma")]
        public string FechaFirma { get; set; }

        [JsonProperty("fechaActivacionFCT")]
        public string FechaActivacionFCT { get; set; }

        [JsonProperty("tieneSV")]
        public bool TieneSV { get; set; }
    }
}
