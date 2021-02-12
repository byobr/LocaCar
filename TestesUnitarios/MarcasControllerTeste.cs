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
    public class MarcasControllerTeste : BaseTestes
    {
        public MarcasControllerTeste()
        {
            Configurar();
        }
        [Fact]
        public void ObterTodasMarcas()
        {
            var Retorno = _marcasController.ObterTodasMarcas();

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }


        [Fact]
        public void InserirMarca()
        {
            MarcasDTO Inserir = new MarcasDTO
            {
                Marca = "Citroen"
            };

            Marcas Objeto = new Marcas
            {
                Id = 6,
                Nome = "Citroen"
            };

            _mockMarcasRepositorio.Setup(m =>
            m.Add(Objeto)).Returns(Objeto);

            _mockMarcasBLL.Setup(m =>
            m.Adicionar(Inserir)).Returns(MarcasBLL.Adicionar(Inserir));

            var Retorno = _marcasController.CadastrarMarca(Inserir);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.CREATED);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }

        [Fact]
        public void RemoverMarca()
        {
            int Remover = 6;

            _mockMarcasRepositorio.Setup(m =>
            m.Remove(Remover)).Returns(true);

            _mockMarcasBLL.Setup(m =>
            m.Remover(Remover)).Returns(MarcasBLL.Remover(Remover));

            var Retorno = _marcasController.RemoverMarcas(Remover);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
        }
    }
}
