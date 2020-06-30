using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public partial class MetaAP
    {
        [JsonProperty("idDetalleMetaSaldoAcumuladoAP")]
        public long IdDetalleMetaSaldoAcumuladoAp { get; set; }

        [JsonProperty("nomina")]
        public long Nomina { get; set; }

        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("esFrontera")]
        public bool EsFrontera { get; set; }

        [JsonProperty("esNovato")]
        public bool EsNovato { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("saldoMeta")]
        public string SaldoMeta { get; set; }

        [JsonProperty("comisionEstimada")]
        public string ComisionEstimada { get; set; }
    }
}
