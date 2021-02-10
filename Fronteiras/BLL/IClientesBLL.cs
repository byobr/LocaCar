using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;
using Util;

namespace Fronteiras.BLL
{
    public interface IClientesBLL
    {
        Clientes Adicionar(ClientesDTO Cliente);
        Clientes ObterPorId(int Cliente);
        Clientes ObterPorCpf(string Cpf);
        bool Remover(string Cpf);
    }
}
