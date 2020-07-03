using System;
using GestionFC.Models.Share;
using Newtonsoft.Json;

namespace GestionFC.Models.VisionBoard
{
    public partial class MetaPlantillaComisionSemRequestModel
    {
        [JsonProperty("MetaComisionSem")]
        public MetaComisionSemModel MetaComisionSem { get; set; }
    }
}
