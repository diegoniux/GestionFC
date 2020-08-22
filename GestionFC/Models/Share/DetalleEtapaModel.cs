using System;
using Newtonsoft.Json;
namespace GestionFC.Models.Share
{
    public class DetalleEtapaModel
    {
        [JsonProperty("registroTraspasoId")]
        public int RegistroTraspasoId { get; set; }

        [JsonProperty("etapaId")]
        public int EtapaId { get; set; }

        [JsonProperty("etapaDescripcion")]
        public string EtapaDescripcion { get; set; }

        [JsonProperty("usuario")]
        public int Usuario { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        [JsonProperty("hora")]
        public string Hora { get; set; }
    }
}
