using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class MetaComisionSemModel
    {
        [JsonProperty("IdPeriodo")]
        public long IdPeriodo { get; set; }

        [JsonProperty("Nomina")]
        public long Nomina { get; set; }

        [JsonProperty("ComisionSem")]
        public long ComisionSem { get; set; }
    }
}
