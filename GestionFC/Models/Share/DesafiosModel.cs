using System;
using Newtonsoft.Json;

namespace GestionFC.Models.Share
{
    public class DesafiosModel
    {
        [JsonProperty("bonoTableta")]
        public long BonoTableta { get; set; }

        [JsonProperty("imgBonoTableta")]
        public string ImgBonoTableta { get; set; }

        [JsonProperty("colorBonoTableta")]
        public string ColorBonoTableta { get; set; }

        [JsonProperty("miltiplicador")]
        public long Miltiplicador { get; set; }

        [JsonProperty("imgMultiplicador")]
        public string ImgMultiplicador { get; set; }

        [JsonProperty("colorMultiplicador")]
        public string ColorMultiplicador { get; set; }

        [JsonProperty("bonoBisemanal")]
        public long BonoBisemanal { get; set; }

        [JsonProperty("imgBonoBisemanal")]
        public string ImgBonoBisemanal { get; set; }

        [JsonProperty("colorBonoBisemanal")]
        public string ColorBonoBisemanal { get; set; }

        [JsonProperty("prospectos")]
        public long Prospectos { get; set; }

        [JsonProperty("imgProspectos")]
        public string ImgProspectos { get; set; }

        [JsonProperty("colorProspectos")]
        public string ColorProspectos { get; set; }

        [JsonProperty("metaComercial")]
        public long MetaComercial { get; set; }

        [JsonProperty("imgMetaComercial")]
        public string ImgMetaComercial { get; set; }

        [JsonProperty("colorMetaComercial")]
        public string ColorMetaComercial { get; set; }

        [JsonProperty("semanasMeta")]
        public long SemanasMeta { get; set; }

        [JsonProperty("imgSemanasMeta")]
        public string ImgSemanasMeta { get; set; }

        [JsonProperty("colorSemanasMeta")]
        public string ColorSemanasMeta { get; set; }
        public DesafiosModel()
        {
        }
    }
}
