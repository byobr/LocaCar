using System;
using System.Collections.Generic;
using System.Text;

namespace Fronteiras.DTOs
{
    public class LoginDTO
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Operador { get; set; }

        public LoginDTO()
        {
            this.Operador = false;
    }
    }
}
