using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Catalogo
{
    public class CatalogoResponseModel
    {
        public Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
    }
}
