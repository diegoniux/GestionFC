using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class MetaDiaModel
    {
        [JsonProperty("saldoLunes")]
        public long SaldoLunes { get; set; }

        [JsonProperty("saldoMartes")]
        public long SaldoMartes { get; set; }

        [JsonProperty("saldoMiercoles")]
        public long SaldoMiercoles { get; set; }

        [JsonProperty("saldoJueves")]
        public long SaldoJueves { get; set; }

        [JsonProperty("saldoViernes")]
        public long SaldoViernes { get; set; }

        [JsonProperty("saldoSabado")]
        public long SaldoSabado { get; set; }

        [JsonProperty("saldoDomingo")]
        public long SaldoDomingo { get; set; }
    }
}
