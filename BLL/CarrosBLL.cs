using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CarrosBLL : ICarrosBLL
    {
        private readonly ICarrosRepositorio _carrosRepositorio;
        private readonly IMarcasRepositorio _marcasRepositorio;
        private readonly IModelosRepositorio _modelosRepositorio;

        public CarrosBLL(
            ICarrosRepositorio carrosRepositorio,
            IMarcasRepositorio marcasRepositorio,
            IModelosRepositorio modelosRepositorio
            )
        {
            this._carrosRepositorio = carrosRepositorio;
            this._marcasRepositorio = marcasRepositorio;
            this._modelosRepositorio = modelosRepositorio;
        }

        public Carros Adicionar(CarrosDTO Carro)
        {
            Modelos Modelo = this._modelosRepositorio.GetById(Carro.ModeloId);
            if (Modelo == null)
                throw new Exception($"Modelo '{Carro.ModeloId}' não encontrada para cadastro do carro.");

            Carros Inserir = new Carros
            {
                AnoModelo = Carro.AnoModelo,
                Categoria = Carro.Categoria,
                Combustivel = Carro.Combustivel,
                LimitePortaMalas = Carro.LimitePortaMalas,
                Marca = Modelo.Marca,
                Modelo = Modelo,
                Placa = Carro.Placa,
                ValorHora = Carro.ValorHora
            };
            Carros Retorno = this._carrosRepositorio.Add(Inserir);
            return Retorno;
        }
        public Carros ObterPorId(int Carro)
        {
            return this._carrosRepositorio.GetById(Carro);
        }
        public List<Carros> ObterTodos()
        {
            return this._carrosRepositorio.GetAll();
        }
        public bool Remover(int Carro)
        {
            return this._carrosRepositorio.Remove(Carro);
        }
    }
}
