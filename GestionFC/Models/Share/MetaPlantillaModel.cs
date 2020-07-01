using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public partial class MetaPlantillaModel
    {
        [JsonProperty("idMetaSaldoAcumuladoGerenteIndividual")]
        public long IdMetaSaldoAcumuladoGerenteIndividual { get; set; }

        [JsonProperty("existeConfiguracionIndividual")]
        public bool ExisteConfiguracionIndividual { get; set; }

        [JsonProperty("saldoAcumuladoMeta")]
        public string SaldoAcumuladoMeta { get; set; }

        [JsonProperty("saldoAcumuladoTetra")]
        public string SaldoAcumuladoTetra { get; set; }

        [JsonProperty("traspasos")]
        public long Traspasos { get; set; }

        [JsonProperty("traspasosFCT")]
        public long TraspasosFct { get; set; }

        [JsonProperty("comisionSem")]
        public string ComisionSem { get; set; }

        [JsonProperty("bonoDesarrollo")]
        public string BonoDesarrollo { get; set; }

        [JsonProperty("bonoExcelencia")]
        public string BonoExcelencia { get; set; }

        [JsonProperty("semanaTetraSemana")]
        public long SemanaTetraSemana { get; set; }

        [JsonProperty("maxSemanas")]
        public long MaxSemanas { get; set; }

        [JsonProperty("totalTetra")]
        public string TotalTetra { get; set; }

    }
}
