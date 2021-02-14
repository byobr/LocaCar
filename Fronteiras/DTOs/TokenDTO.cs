using System;
using System.Collections.Generic;
using System.Text;

namespace Fronteiras.DTOs
{
    public class TokenDTO
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Perfil { get; set; }
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
    }
}
