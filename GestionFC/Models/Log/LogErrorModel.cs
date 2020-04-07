using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Log
{
    public class LogErrorModel
    {

        public int IdPantalla { get; set; }
        public int Usuario { get; set; }
        public string Error { get; set; }
        public string Dispositivo { get; set; }
    }
}
