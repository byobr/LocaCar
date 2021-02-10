using Entidades;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface ICarrosRepositorio
    {
        Carros Add(Carros Carro);
        Carros GetById(int Carro);
        List<Carros> GetAll();
        bool Remove(int Carro);
    }
}
