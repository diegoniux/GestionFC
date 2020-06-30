﻿using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.VisionBoard
{
    public class MetaPlantillaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("idMetaSaldoAcumuladoGerenteIndividual")]
        public int IdMetaSaldoAcumuladoGerenteIndividual { get; set; }

        [JsonProperty("saldoAcumuladoMeta")]
        public int SaldoAcumuladoMeta { get; set; }
    }
}
