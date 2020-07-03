using System;
using System.Collections.Generic;

using System.Globalization;
using GestionFC.Models.Share;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GestionFC.Models.VisionBoard
{
    public partial class MetaPlantillaSaldoAcumuladoResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("idMetaSaldoAcumuladoGerenteIndividual")]
        public long IdMetaSaldoAcumuladoGerenteIndividual { get; set; }

        [JsonProperty("saldoAcumuladoMeta")]
        public long SaldoAcumuladoMeta { get; set; }
    }
}
