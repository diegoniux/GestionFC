using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GestionFC.Models.Share;
using Newtonsoft.Json;

namespace GestionFC.Models.Alertas
{
    public class FoliosPendientesSVResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("resultDatos")]
        public ObservableCollection<FoliosPendientesSVModel> ResultDatos { get; set; }
    }
}
