using Entidades;
using Fronteiras.DTOs;
using System.Collections.Generic;

namespace Fronteiras.BLL
{
    public interface IAlugueisBLL
    {
        SimulacaoAluguelDTO Simular(SimularAlugueisDTO Aluguel);
        Alugueis Alugar(AlugueisDTO Aluguel);
        Alugueis ObterPorId(int Aluguel);
        List<Alugueis> ObterPorCliente(int Cliente);
        bool Remover(int Aluguel);
    }
}
