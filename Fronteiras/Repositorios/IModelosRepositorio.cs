using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface IModelosRepositorio
    {
        Modelos Add(Modelos Modelo);
        List<Modelos> GetAll();
        Modelos GetById(int Modelo);
        bool Remove(int Modelo);
    }
}
