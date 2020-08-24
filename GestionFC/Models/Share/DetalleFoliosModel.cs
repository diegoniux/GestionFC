using System;
using Newtonsoft.Json;
namespace GestionFC.Models.Share
{
    public class DetalleFoliosModel
    {
        [JsonProperty("registroTraspasoId")]
        public long RegistroTraspasoId { get; set; }

        [JsonProperty("folioSolicitud")]
        public string FolioSolicitud { get; set; }

        [JsonProperty("estatusId")]
        public long EstatusId { get; set; }

        [JsonProperty("estatusDescripcion")]
        public string EstatusDescripcion { get; set; }

        [JsonProperty("seccion")]
        public long Seccion { get; set; }

        [JsonProperty("porcentaje")]
        public string Porcentaje { get; set; }

        [JsonProperty("imgDocumental")]
        public string ImgDocumental { get; set; }

        [JsonProperty("imgInvestigacion")]
        public string ImgInvestigacion { get; set; }

        [JsonProperty("imgProcesar")]
        public string ImgProcesar { get; set; }

        [JsonProperty("imgSaldoVirtual")]
        public string ImgSaldoVirtual { get; set; }

        [JsonProperty("textDocumental")]
        public string TextDocumental { get; set; }

        [JsonProperty("textInvestigacion")]
        public string TextInvestigacion { get; set; }

        [JsonProperty("textProcesar")]
        public string TextProcesar { get; set; }

        [JsonProperty("textSaldoVirtual")]
        public string TextSaldoVirtual { get; set; }

        [JsonProperty("colorDocumental")]
        public string ColorDocumental { get; set; }

        [JsonProperty("colorInvestigacion")]
        public string ColorInvestigacion { get; set; }

        [JsonProperty("colorProcesar")]
        public string ColorProcesar { get; set; }

        [JsonProperty("colorSaldoVirtua")]
        public string ColorSaldoVirtua { get; set; }
    }
}
