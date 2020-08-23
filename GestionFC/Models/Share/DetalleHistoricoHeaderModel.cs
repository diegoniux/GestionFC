using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class DetalleHistoricoHeaderModel
    {
        [JsonProperty("habilitarAdelantar")]
        public bool HabilitarAdelantar { get; set; }

        [JsonProperty("imgAdelantar")]
        public string ImgAdelantar { get; set; }

        [JsonProperty("fechaAdelantar")]
        public DateTime FechaAdelantar { get; set; }

        [JsonProperty("fechaIniFin")]
        public string FechaIniFin { get; set; }

        [JsonProperty("habilitarAnterior")]
        public bool HabilitarAnterior { get; set; }

        [JsonProperty("imgAnterior")]
        public string ImgAnterior { get; set; }

        [JsonProperty("fechaAnterior")]
        public DateTime FechaAnterior { get; set; }
    }
}
