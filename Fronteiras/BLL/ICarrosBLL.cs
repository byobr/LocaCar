using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;
using Util;

namespace Fronteiras.BLL
{
    public interface ICarrosBLL
    {
        Carros Adicionar(CarrosDTO Cliente);
        Carros ObterPorId(int Carrro);
        List<Carros> ObterTodos();
        bool Remover(int Carro);
    }
}
