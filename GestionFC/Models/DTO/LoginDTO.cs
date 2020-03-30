using GestionFC.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.DTO
{
    public class LoginDTO
    {
        public LoginModel Login { get; set; }
        public bool RememberUser { get; set; }
    }
}
