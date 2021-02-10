using Entidades;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface IMarcasRepositorio
    {
        Marcas Add(Marcas Marca);
        List<Marcas> GetAll();
        Marcas GetById(int Marca);
        bool Remove(int Marca);
    }
}
