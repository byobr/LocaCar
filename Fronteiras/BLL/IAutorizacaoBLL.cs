using Fronteiras.DTOs;
using System;

namespace Fronteiras.BLL
{
    public interface IAutorizacaoBLL
    {
        Tuple<string,DateTime> GerarToken(UsuarioDTO Usuario);
        TokenDTO Login(LoginDTO Usuario);
    }
}
