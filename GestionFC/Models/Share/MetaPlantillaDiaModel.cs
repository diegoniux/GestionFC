using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class MetaPlantillaDiaModel
    {
        [JsonProperty("IdPeriodo")]
        public long IdPeriodo { get; set; }

        [JsonProperty("Nomina")]
        public long Nomina { get; set; }

        [JsonProperty("SaldoAcumuladoMeta")]
        public long SaldoAcumuladoMeta { get; set; }

        [JsonProperty("Dia")]
        public long Dia { get; set; }
    }
}
