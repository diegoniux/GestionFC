using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using GestionFC.Models.Share;
using System.Collections.ObjectModel;

namespace GestionFC.Models.Alertas
{
    public class PlantillaImproductivaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("cantidad")]
        public int cantidad { get; set; }

        [JsonProperty("resultDatos")]
        public ObservableCollection<PlantillaImproductivaModel> ResultDatos { get; set; }

    }
}
