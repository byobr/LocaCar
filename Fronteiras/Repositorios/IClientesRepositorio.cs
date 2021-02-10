using Entidades;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface IClientesRepositorio
    {
        Clientes Add(Clientes Marca);
        Clientes GetById(int Cliente);
        Clientes GetByCpf(string Cpf);
        bool Remove(string Cpf);
    }
}
