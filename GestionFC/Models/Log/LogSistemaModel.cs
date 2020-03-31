using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Login
{
    public class LogSistemaModel
    {
        public int IdAccion { get; set; }
        public int IdPantalla { get; set; }
        public int Usuario { get; set; }
        public string Dispositivo { get; set; }
    }
}
