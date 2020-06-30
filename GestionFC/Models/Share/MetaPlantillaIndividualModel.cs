using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class MetaPlantillaIndividualModel
    {
        [JsonProperty("NominaAP")]
        public long NominaAp { get; set; }

        [JsonProperty("Nomina")]
        public long Nomina { get; set; }

        [JsonProperty("SaldoAcumuladoMeta")]
        public long SaldoAcumuladoMeta { get; set; }
    }
}
