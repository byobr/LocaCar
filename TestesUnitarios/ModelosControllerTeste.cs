using Entidades;
using Fronteiras.DTOs;
using LocaCar.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Util;
using Xunit;

namespace TestesUnitarios
{
    public class ModelosControllerTeste : BaseTestes
    {
        public ModelosControllerTeste()
        {
            Configurar();
        }
        [Fact]
        public void ObterTodasModelos()
        {
            var Retorno = _modelosController.ObterTodosModelos();

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }


        [Fact]
        public void InserirModelo()
        {
            ModelosDTO Inserir = new ModelosDTO
            {
                MarcaId = 1,
                NomeModelo = "Idea"
            };

            Modelos Objeto = new Modelos
            {
                Id = 3,
                Modelo = "Idea",
                Marca = new Marcas
                {
                    Id = 1,
                    Nome = "Fiat"
                }
            };

            _mockModelosRepositorio.Setup(m =>
             m.Add(Objeto)).Returns(Objeto);

            List<Marcas> Marcas = new List<Marcas>
            {
                new Marcas{Id = 1, Nome = "Fiat"},
                new Marcas{Id = 2, Nome = "Ford"},
                new Marcas{Id = 3, Nome = "VolksWagen"}
            };

            _mockMarcasBLL.Setup(m =>
            m.ObterTodos()).Returns(MarcasBLL.ObterTodos());

            _mockMarcasRepositorio.Setup(m =>
            m.GetAll()).Returns(Marcas);

            _mockModelosRepositorio.Setup(m =>
            m.Add(Objeto)).Returns(Objeto);

            var Retorno2 = _modelosController.CadastrarModelo(Inserir);

            var Saida2 = RetornarSaidaApi(Retorno2, TipoSaidaApiEnum.CREATED);

            Assert.True(Saida2.ExecutadoComSucesso);
            Assert.NotNull(Saida2);
        }

        [Fact]
        public void RemoverMOdelo()
        {
            int Remover = 1;

            _mockModelosRepositorio.Setup(m =>
            m.Remove(Remover)).Returns(true);

            _mockModelosBLL.Setup(m =>
            m.Remover(Remover)).Returns(ModelosBLL.Remover(Remover));

            var Retorno = _modelosController.RemoverModelo(Remover);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.NOTFOUND);

            Modelos Objeto = new Modelos
            {
                Id = 3,
                Modelo = "Idea",
                Marca = new Marcas
                {
                    Id = 1,
                    Nome = "Fiat"
                }
            };

            _mockModelosRepositorio.Setup(m =>
            m.GetById(Remover)).Returns(Objeto);

            _mockModelosBLL.Setup(m =>
            m.ObterPorId(Remover)).Returns(ModelosBLL.ObterPorId(Remover));

            var Retorno2 = _modelosController.RemoverModelo(Remover);

            var Saida2 = RetornarSaidaApi(Retorno2, TipoSaidaApiEnum.OK);

            Assert.True(Saida2.ExecutadoComSucesso);
        }
    }
}
