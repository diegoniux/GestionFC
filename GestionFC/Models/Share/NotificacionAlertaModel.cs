using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class NotificacionAlertaModel
    {
        [JsonProperty("notiImprod")]
        public int NotiImprod { get; set; }

        [JsonProperty("notiRecuperacion")]
        public int NotiRecuperacion { get; set; }

        [JsonProperty("notiInv")]
        public int NotiInv { get; set; }

        [JsonProperty("notiSV")]
        public int NotiSV { get; set; }
    }
}
