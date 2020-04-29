using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ProduccionDiariaModel
    {
            [JsonProperty("nombreCompletoAP")]
            public string NombreCompletoAp { get; set; }

            [JsonProperty("saldoVirtualLunes")]
            public int SaldoVirtualLunes { get; set; }

            [JsonProperty("saldoVirtualMartes")]
            public int SaldoVirtualMartes { get; set; }

            [JsonProperty("saldoVirtualMiercoles")]
            public int SaldoVirtualMiercoles { get; set; }

            [JsonProperty("saldoVirtualJueves")]
            public int SaldoVirtualJueves { get; set; }

            [JsonProperty("saldoVirtualViernes")]
            public int SaldoVirtualViernes { get; set; }

            [JsonProperty("saldoVirtualSabado")]
            public int SaldoVirtualSabado { get; set; }

            [JsonProperty("saldoVirtualDomingo")]
            public int SaldoVirtualDomingo { get; set; }

            [JsonProperty("saldoVirtualSemana")]
            public int SaldoVirtualSemana { get; set; }

            [JsonProperty("indicadorSaldoMeta")]
            public string IndicadorSaldoMeta { get; set; }

            [JsonProperty("saldoVirtualFaltante")]
            public int SaldoVirtualFaltante { get; set; }

            [JsonProperty("metaSemana")]
            public int MetaSemana { get; set; }

            [JsonProperty("fctInactivos")]
            public int FctInactivos { get; set; }

            [JsonProperty("fctActivos")]
            public int FctActivos { get; set; }

            [JsonProperty("foliosMenores30k")]
            public int FoliosMenores30K { get; set; }

            [JsonProperty("foliosCertificados")]
            public int FoliosCertificados { get; set; }
    }
}
