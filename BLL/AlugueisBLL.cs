using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AlugueisBLL : IAlugueisBLL
    {
        private readonly IAlugueisRepositorio _alugueisRepositorio;
        private readonly ICarrosRepositorio _carrosRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;

        public AlugueisBLL(IAlugueisRepositorio alugueisRepositorio, ICarrosRepositorio carrosRepositorio,
            IClientesRepositorio clientesRepositorio)
        {
            this._alugueisRepositorio = alugueisRepositorio;
            this._carrosRepositorio = carrosRepositorio;
            this._clientesRepositorio = clientesRepositorio;
        }

        public SimulacaoAluguelDTO Simular(SimularAlugueisDTO Aluguel)
        {
            Carros Carro = this._carrosRepositorio.GetById(Aluguel.CarroId);

            if (Carro == null)
                throw new Exception($"Carro com id: {Aluguel.CarroId} não encontrado.");

            Alugueis AluguelSimulacao = new Alugueis
            {
                Carro = Carro,
                Cliente = null,
                DataAluguel = DateTime.Now,
                DataCheckin = Aluguel.DataCheckin,
                DataCheckout = Aluguel.DataCheckout,
                Origem = Aluguel.Origem,
                PrecoNoDia = Carro.ValorHora
            };

            return new SimulacaoAluguelDTO
            {
                Carro = Carro,
                DateCheckin = AluguelSimulacao.DataCheckin,
                DateCheckout = AluguelSimulacao.DataCheckout,
                Horas = AluguelSimulacao.Horas,
                PrecoTotal = AluguelSimulacao.PrecoTotal
            };
        }
        public Alugueis Alugar(AlugueisDTO Aluguel)
        {
            Carros Carro = this._carrosRepositorio.GetById(Aluguel.CarroId);

            if (Carro == null)
                throw new Exception($"Carro com id: {Aluguel.CarroId} não encontrado.");

            Clientes Cliente = this._clientesRepositorio.GetById(Aluguel.ClienteId);

            if (Cliente == null)
                throw new Exception($"Cliente com id: {Aluguel.ClienteId} não encontrado.");


            return _alugueisRepositorio.Add(new Alugueis
            {
                Carro = Carro,
                Cliente = Cliente,
                DataAluguel = DateTime.Now,
                DataCheckin = Aluguel.DataCheckin,
                DataCheckout = Aluguel.DataCheckout,
                Origem = Aluguel.Origem,
                PrecoNoDia = Carro.ValorHora
            });
        }

        public Alugueis ObterPorId(int Aluguel)
        {
            return _alugueisRepositorio.GetById(Aluguel);
        }

        public List<Alugueis> ObterPorCliente(int Cliente)
        {
            return _alugueisRepositorio.GetByClientId(Cliente);
        }

        public bool Remover(int Aluguel)
        {
            return _alugueisRepositorio.Remove(Aluguel);

        }
    }
}
