namespace GestionFC.Models.VisionBoard
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using GestionFC.Models.Share;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetMetaPlantillaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("metaPlantilla")]
        public MetaPlantillaModel MetaPlantilla { get; set; }

        [JsonProperty("fechasSemana")]
        public FechasSemanaModel FechasSemana { get; set; }

        [JsonProperty("detalleMetaPorDia")]
        public MetaDiaModel DetalleMetaPorDia { get; set; }
    }
}
