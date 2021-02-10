using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;
using Util;

namespace Fronteiras.BLL
{
    public interface IOperadoresBLL
    {
        Operadores Adicionar(OperadoresDTO Operador);
        Operadores ObterPorId(int Operador);
        Operadores ObterPorMatricula(int Matricula);
        bool Remover(int Matricula);
    }
}
