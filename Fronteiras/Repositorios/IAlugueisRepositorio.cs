using Entidades;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface IAlugueisRepositorio
    {
        Alugueis Add(Alugueis Aluguel);
        Alugueis GetById(int Aluguel);
        List<Alugueis> GetByClientId(int Cliente);
        bool Remove(int Aluguel);
    }
}
