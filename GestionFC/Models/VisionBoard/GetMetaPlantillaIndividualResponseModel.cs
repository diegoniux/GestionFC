namespace GestionFC.Models.VisionBoard
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using GestionFC.Models.Share;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetMetaPlantillaIndividualResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("comisionEstimada")]
        public string ComisionEstimada { get; set; }

        [JsonProperty("listMetaAP")]
        public List<MetaAP> ListMetaAp { get; set; }
    }
}
