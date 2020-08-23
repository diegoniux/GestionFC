using System;
namespace GestionFC.Models.Alertas
{
    public class MensajeGerenteResponseModel
    {
        public Models.Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string Mensaje { get; set; }
        public string SaldoAcomuladoMeta { get; set; }

        public MensajeGerenteResponseModel()
        {
            this.ResultadoEjecucion = new Models.Share.ResultadoEjecucion();
        }
    }
}
