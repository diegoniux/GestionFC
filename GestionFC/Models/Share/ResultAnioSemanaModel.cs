using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ResultAnioSemanaModel
    {
        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("semanaAnio")]
        public int SemanaAnio { get; set; }

        [JsonProperty("esActual")]
        public bool EsActual { get; set; }

        [JsonProperty("fechaCorte")]
        public DateTime FechaCorte { get; set; }

        [JsonProperty("esUltimaFechaCorte")]
        public bool EsUltimaFechaCorte { get; set; }

        [JsonProperty("totalSaldoVirtualLunes")]
        public int TotalSaldoVirtualLunes { get; set; }

        [JsonProperty("totalSaldoVirtualMartes")]
        public int TotalSaldoVirtualMartes { get; set; }

        [JsonProperty("totalSaldoVirtualMiercoles")]
        public int TotalSaldoVirtualMiercoles { get; set; }

        [JsonProperty("totalSaldoVirtualJueves")]
        public int TotalSaldoVirtualJueves { get; set; }

        [JsonProperty("totalSaldoVirtualViernes")]
        public int TotalSaldoVirtualViernes { get; set; }

        [JsonProperty("totalSaldoVirtualSabado")]
        public int TotalSaldoVirtualSabado { get; set; }

        [JsonProperty("totalSaldoVirtualDomingo")]
        public int TotalSaldoVirtualDomingo { get; set; }

        [JsonProperty("totalSaldoVirtualSemana")]
        public int TotalSaldoVirtualSemana { get; set; }

        [JsonProperty("totalSaldoVirtualFaltante")]
        public int TotalSaldoVirtualFaltante { get; set; }

        [JsonProperty("totalMetaSemana")]
        public int TotalMetaSemana { get; set; }

        [JsonProperty("totalFCTInactivos")]
        public int TotalFCTInactivos { get; set; }

        [JsonProperty("totalFCTActivos")]
        public int TotalFCTActivos { get; set; }

        [JsonProperty("totalFoliosMenores30k")]
        public int TotalFoliosMenores30k { get; set; }

        [JsonProperty("totalFoliosCertificados")]
        public int TotalFoliosCertificados { get; set; }
    }
}
