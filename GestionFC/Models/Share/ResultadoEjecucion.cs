using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class ResultadoEjecucion
    {
        public bool EjecucionCorrecta { get; set; }
        public string ErrorMessage { get; set; }
        public string FriendlyMessage { get; set; }
    }
}
