using System;
using GestionFC.Models.Share;

namespace GestionFC.Models.DetalleEspecialista
{
    public class DetalleEspecialistaResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleEspecialistaModel DetalleEspecialista { get; set; }
        public DesafiosModel Desafios { get; set; }
    }
}
