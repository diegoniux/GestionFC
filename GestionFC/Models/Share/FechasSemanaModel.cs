using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FechasSemanaModel
    {
        [JsonProperty("fechaLunes")]
        public string FechaLunes { get; set; }

        [JsonProperty("fechaMartes")]
        public string FechaMartes { get; set; }

        [JsonProperty("fechaMiercoles")]
        public string FechaMiercoles { get; set; }

        [JsonProperty("fechaJueves")]
        public string FechaJueves { get; set; }

        [JsonProperty("fechaViernes")]
        public string FechaViernes { get; set; }

        [JsonProperty("fechaSabado")]
        public string FechaSabado { get; set; }

        [JsonProperty("fechaDomingo")]
        public string FechaDomingo { get; set; }
    }
}
