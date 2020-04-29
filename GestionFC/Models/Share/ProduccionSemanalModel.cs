using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ProduccionSemanalModel
    {
        [JsonProperty("nombreCompletoAP")]
        public string NombreCompletoAp { get; set; }

        [JsonProperty("saldoVirtualSemana1")]
        public int SaldoVirtualSemana1 { get; set; }

        [JsonProperty("saldoVirtualSemana2")]
        public int SaldoVirtualSemana2 { get; set; }

        [JsonProperty("saldoVirtualSemana3")]
        public int SaldoVirtualSemana3 { get; set; }

        [JsonProperty("saldoVirtualSemana4")]
        public int SaldoVirtualSemana4 { get; set; }

        [JsonProperty("indicadorSaldoMetaSem1")]
        public string IndicadorSaldoMetaSem1 { get; set; }

        [JsonProperty("indicadorSaldoMetaSem2")]
        public string IndicadorSaldoMetaSem2 { get; set; }

        [JsonProperty("indicadorSaldoMetaSem3")]
        public string IndicadorSaldoMetaSem3 { get; set; }

        [JsonProperty("indicadorSaldoMetaSem4")]
        public string IndicadorSaldoMetaSem4 { get; set; }

        [JsonProperty("saldoVirtualTetrasemana")]
        public long SaldoVirtualTetrasemana { get; set; }

        [JsonProperty("indicadorSaldoMetaTetra")]
        public string IndicadorSaldoMetaTetra { get; set; }
    }
}
