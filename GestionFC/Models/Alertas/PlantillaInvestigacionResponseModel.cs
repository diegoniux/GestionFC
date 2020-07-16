using GestionFC.Models.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GestionFC.Models.Alertas
{
    public class PlantillaInvestigacionResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("cantidad")]
        public int cantidad { get; set; }

        [JsonProperty("resultDatos")]
        public ObservableCollection<PlantillaInvestigacionModel> ResultDatos { get; set; }
    }
}
