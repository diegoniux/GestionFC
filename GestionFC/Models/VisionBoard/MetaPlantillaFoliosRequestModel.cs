﻿using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text; 

namespace GestionFC.Models.VisionBoard
{
    public class MetaPlantillaFoliosRequestModel
    {
        [JsonProperty("MetaPlantillaFolios")]
        public MetaPlantillaFoliosModel MetaPlantillaFolios { get; set; }
    }
}