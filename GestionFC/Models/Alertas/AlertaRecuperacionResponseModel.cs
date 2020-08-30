using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GestionFC.Models.Alertas
{
    public class AlertaRecuperacionResponseModel
    {
        public Models.Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        //public List<Models.Share.AlertaRecuperacion> ResultDatos { get; set; }
        public List<Models.Share.AlertaRecuperacionPantallas> Pantallas { get; set; }
        public List<Models.Share.AlertaRecuperacionPreguntas> Preguntas { get; set; }
        public List<Models.Share.AlertaRecuperacionDocumentos> Documentos { get; set; }

        public AlertaRecuperacionResponseModel()
        {
            this.ResultadoEjecucion = new Models.Share.ResultadoEjecucion();
            //this.ResultDatos = new List<Models.Share.AlertaRecuperacion>();
            this.Pantallas = new List<Models.Share.AlertaRecuperacionPantallas>();
            this.Preguntas = new List<Models.Share.AlertaRecuperacionPreguntas>();
            this.Documentos = new List<Models.Share.AlertaRecuperacionDocumentos>();
       
        }
    }
}
