using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;

namespace Fronteiras.BLL
{
    public interface IMarcasBLL
    {
        Marcas Adicionar(MarcasDTO Marca);
        List<Marcas> ObterTodos();
        bool Remover(int Marca);
    }
}
