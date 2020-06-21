﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using GestionFC.Models.Share;

namespace GestionFC.Models.Alertas
{
    public class PlantillaImproductivaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("resultDatos")]
        public IList<PlantillaImproductivaModel> ResultDatos { get; set; }

    }
}
