using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class MetaPlantillaFoliosModel
    {
        [JsonProperty("Nomina")]
        public long Nomina { get; set; }

        [JsonProperty("Folios")]
        public long Folios { get; set; }

        [JsonProperty("FoliosFCT")]
        public long FoliosFct { get; set; }
    }
}
