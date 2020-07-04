using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public partial class MetaSaldoAcumuladoModel
    {
        [JsonProperty("IdPeriodo")]
        public long IdPeriodo { get; set; }

        [JsonProperty("Nomina")]
        public long Nomina { get; set; }

        [JsonProperty("SaldoAcumuladoMeta")]
        public long SaldoAcumuladoMeta { get; set; }
    }
}
