using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.VisionBoard
{
    public partial class MetaPlantillaRequestModel
    {
        [JsonProperty("MetaPlantillaDia")]
        public MetaPlantillaDiaModel MetaPlantillaDia { get; set; }
    }
}
