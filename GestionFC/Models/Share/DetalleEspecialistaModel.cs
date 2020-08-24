using System;
using Newtonsoft.Json;
using SkiaSharp;

namespace GestionFC.Models.Share
{
    public class DetalleEspecialistaModel
    {
        [JsonProperty("nominaPromotor")]
        public int NominaPromotor { get; set; }

        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("genero")]
        public string Genero { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("mesesLaborando")]
        public string MesesLaborando { get; set; }

        [JsonProperty("saldoAcumulado")]
        public string SaldoAcumulado { get; set; }

        [JsonProperty("saldoMeta")]
        public string SaldoMeta { get; set; }

        private string porcentajeSaldoacumulado;
        [JsonProperty("porcentajeSaldoAcumulado")]
        public string PorcentajeSaldoAcumulado
        {
            get
            {
                return porcentajeSaldoacumulado;
            }
            set
            {
                porcentajeSaldoacumulado = value;
                try
                {
                    PorcentajeSaldoAcumuladoDesc = decimal.Parse(value).ToString("0%");
                }
                catch (Exception ex)
                {

                }
                
            }
        }

        [JsonProperty("imagenSaldoAcumulado")]
        public string ImagenSaldoAcumulado { get; set; }

        [JsonProperty("nivelComision")]
        public long NivelComision { get; set; }

        [JsonProperty("tramitesPorRecuperar")]
        public long TramitesPorRecuperar { get; set; }

        [JsonProperty("tramitesEnCalidad")]
        public long TramitesEnCalidad { get; set; }


        // Propiedad Calculada
        public string PorcentajeSaldoAcumuladoDesc { get; set; }




    }
}
