using Entidades;
using System.Collections.Generic;

namespace Fronteiras.Repositorios
{
    public interface IOperadoresRepositorio
    {
        Operadores Add(Operadores Marca);
        Operadores GetById(int Operador);
        Operadores GetByLogin(int Matricula);
        bool Remove(int Operador);
    }
}
