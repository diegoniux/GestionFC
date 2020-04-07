using GestionFC.Models.Share;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Login
{
    public class LoginResponseModel
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string Token { get; set; }
        public bool UsuarioAutorizado { get; set; }
        public bool Activo { get; set; }
        public bool EsGerente { get; set; }
    }
}
