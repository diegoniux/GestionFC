using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.SqLite.DBModel
{
    public class GestionFCModel
    {
        public int UserSaved { get; set; }
        public int Nomina { get; set; }
        public string NombreUsuario { get; set; }
        public string TokenSesion { get; set; }
    }
}
