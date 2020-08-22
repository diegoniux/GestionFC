using System;
using Newtonsoft.Json;
namespace GestionFC.Models.Share
{
    public class DetalleFoliosModel
    {
        [JsonProperty("registroTraspasoId")]
        public long RegistroTraspasoId { get; set; }

        [JsonProperty("folioSolicitud")]
        public string FolioSolicitud { get; set; }

        [JsonProperty("estatusId")]
        public int EstatusId { get; set; }

        [JsonProperty("estatusDescripcion")]
        public string EstatusDescripcion { get; set; }

        [JsonProperty("seccion")]
        public int Seccion { get; set; }
    }
}
