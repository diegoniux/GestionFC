using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.SqLite.DBModel
{
    public class GestionFCModel
    {
        private int userSaved;

        public int UserSaved { get => userSaved; set => userSaved = value; }
        public int Nomina { get; set; }
        public string NombreUsuario { get; set; }
        public string TokenSesion { get; set; }
    }
}
