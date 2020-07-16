using System;
using System.Collections.Generic;
using System.Text;
using GestionFC.Models.Share;
using Newtonsoft.Json;

namespace GestionFC.Models.Alertas
{
    public class PlantillaRecuperacionModel
    {
        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("idAlerta")]
        public int IdAlerta { get; set; }
        
        [JsonProperty("idEstatusAlerta")]
        public int IdEstatusAlerta { get; set; }
        
        [JsonProperty("nominaAp")]
        public int NominaAp { get; set; }
        
        [JsonProperty("nombreAP")]
        public string NombreAP { get; set; }
        
        [JsonProperty("apellidosAP")]
        public string ApellidosAP { get; set; }

        [JsonProperty("validacionesRecuperacion")]
        public int ValidacionesRecuperacion { get; set; }

        [JsonProperty("msj1")]
        public string Msj1 { get; set; }

        [JsonProperty("msj2")]
        public string Msj2 { get; set; }
        
        [JsonProperty("msj3")]
        public string Msj3 { get; set; }

        [JsonProperty("banderaCalendar")]
        public bool BanderaCalendar { get; set; }

        [JsonProperty("colorCalendar")]
        public string colorCalendar { get; set; }

        [JsonProperty("msjEstatus")]
        public string MsjEstatus { get; set; }

        [JsonProperty("imgNotificacion")]
        public string ImgNotificacion { get; set; }

        [JsonProperty("imgWarning")]
        public bool ImgWarning { get; set; }

        [JsonProperty("foliosRecuperacion")]
        public List<FoliosRecuperacion> FoliosRecuperacion { get; set; }



    }
}
