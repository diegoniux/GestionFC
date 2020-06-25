using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GestionFC.Models.Share;
using Newtonsoft.Json;

namespace GestionFC.Models.Alertas
{
    public class NotificacionAlertaResponseModel
    {
        [JsonProperty("resultadoEjecucion")]
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        [JsonProperty("resultDatos")]
        public ObservableCollection<NotificacionAlertaModel> ResultDatos { get; set; }
    }
}
