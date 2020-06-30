using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.VisionBoard
{
    public class MetaPlantillaIndividualRequestModel
    {
        [JsonProperty("MetaPlantillaIndividual")]
        public MetaPlantillaIndividualModel MetaPlantillaIndividual { get; set; }
    }
}
