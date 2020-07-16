using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class MotivoRechazoModel
    {
        [JsonProperty("pantalla")]
        public string Pantalla { get; set; }

        [JsonProperty("motivo")]
        public int Motivo { get; set; }
    }
}
