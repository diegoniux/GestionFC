using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FolioSolicitud
    {
        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("registroTraspasoId")]
        public string RegistroTraspasoId { get; set; }

        [JsonProperty("motivos")]
        public List<MotivoRechazoModel> Motivos { set; get; }
    }
}
