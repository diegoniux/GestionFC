using System;
using GestionFC.Models.Share;
using Newtonsoft.Json;

namespace GestionFC.Models.VisionBoard
{
    public class MetaPlantillaSaldoAcumuladoRequestModel
    {
        [JsonProperty("MetaSaldoAcumulado")]
        public MetaSaldoAcumulado MetaSaldoAcumulado { get; set; }
    }
}
