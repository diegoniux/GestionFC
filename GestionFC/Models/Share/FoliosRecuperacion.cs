using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FoliosRecuperacion
    {
        [JsonProperty("folioSolicitud")]
        public string FolioSolicitud { get; set; }

        [JsonProperty("motivos")]
        public List<MotivoRechazoModel> Motivos { set; get; }
    }
}
