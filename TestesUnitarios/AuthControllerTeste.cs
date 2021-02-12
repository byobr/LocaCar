using Entidades;
using Fronteiras.DTOs;
using LocaCar.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Util;
using Xunit;

namespace TestesUnitarios
{
    public class AuthControllerTeste : BaseTestes
    {
        public AuthControllerTeste()
        {
            Configurar();
        }
        [Fact]
        public void ObterCliente()
        {

            _mockClientesBLL.Setup(m => m.ObterPorCpf(It.IsAny<string>())).Returns(ClientesBLL.ObterPorCpf("21483212009"));

            var Retorno = _authController.ObterPorCpf("21483212009");

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }

        [Fact]
        public void ObterOperador()
        {
            _mockOperadoresBLL.Setup(m =>
            m.ObterPorMatricula(It.IsAny<int>())).Returns(OperadoresBLL.ObterPorMatricula(123));

            var Retorno = _authController.ObterPorMatricula(123);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }


        [Fact]
        public void InserirCliente()
        {
            ClientesDTO Inserir = new ClientesDTO
            {
                Nome = "Joao",
                Aniversaio = DateTime.Now,
                Celular = "31988887777",
                Cep = "30000-000",
                Cidade = "BH",
                Complemento = "TAL",
                Cpf = "21483212009",
                Email = "joao@locacar.com",
                Estado = "MG",
                Logradouro = "Rua Tal",
                Numero = "123 Tal",
                Senha = "teste"
            };

            Clientes Objeto = new Clientes
            {
                Id = 1,
                Nome = "Joao",
                Aniversaio = DateTime.Now,
                Celular = "31988887777",
                Cep = "30000-000",
                Cidade = "BH",
                Complemento = "TAL",
                Cpf = "21483212009",
                Email = "joao@locacar.com",
                Estado = "MG",
                Logradouro = "Rua Tal",
                Numero = "123 Tal",
                Senha = "698dc19d489c4e4db73e28a713eab07b" //teste
            };

            _mockClientesRepositorio.Setup(m =>
            m.Add(Objeto)).Returns(Objeto);

            _mockClientesBLL.Setup(m =>
            m.Adicionar(Inserir)).Returns(ClientesBLL.Adicionar(Inserir));

            var Retorno = _authController.CadastrarCliente(Inserir);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.CREATED);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }

        [Fact]
        public void RemoverCliente()
        {

            _mockClientesBLL.Setup(m => m.ObterPorCpf(It.IsAny<string>())).Returns(ClientesBLL.ObterPorCpf("21483212009"));

            string Remover = "21483212009";

            _mockClientesBLL.Setup(m => m.Remover(It.IsAny<string>())).Returns(ClientesBLL.Remover(Remover));

            _mockClientesRepositorio.Setup(m =>
            m.Remove(Remover)).Returns(true);

            _mockClientesBLL.Setup(m =>
            m.Remover(Remover)).Returns(ClientesBLL.Remover(Remover));

            var Retorno2 = _authController.RemoverCliente(Remover);

            var Saida2 = RetornarSaidaApi(Retorno2, TipoSaidaApiEnum.OK);

            Assert.True(Saida2.ExecutadoComSucesso);
        }

        [Fact]
        public void InserirOperador()
        {
            var Objeto = new Operadores { Id = 1, Nome = "Operador1", Matricula = 123, Senha = "698dc19d489c4e4db73e28a713eab07b" /*teste*/ };

            var Inserir = new OperadoresDTO { Nome = "Operador1", Matricula = 123, Senha = "698dc19d489c4e4db73e28a713eab07b" /*teste*/ };

            _mockOperadoresRepositorio.Setup(m =>
            m.Add(Objeto)).Returns(Objeto);

            _mockOperadoresBLL.Setup(m =>
            m.Adicionar(Inserir)).Returns(OperadoresBLL.Adicionar(Inserir));

            var Retorno = _authController.CadastrarOperador(Inserir);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.CREATED);

            Assert.True(Saida.ExecutadoComSucesso);
            Assert.NotNull(Saida);
        }

        [Fact]
        public void RemoverOperador()
        {
            int Remover = 123;

            _mockOperadoresBLL.Setup(m =>
            m.ObterPorMatricula(It.IsAny<int>())).Returns(OperadoresBLL.ObterPorMatricula(123));

            _mockOperadoresRepositorio.Setup(m =>
            m.Remove(Remover)).Returns(true);

            _mockOperadoresBLL.Setup(m =>
            m.Remover(Remover)).Returns(OperadoresBLL.Remover(Remover));

            var Retorno = _authController.RemoverOperador(Remover);

            var Saida = RetornarSaidaApi(Retorno, TipoSaidaApiEnum.OK);

            Assert.True(Saida.ExecutadoComSucesso);
        }
    }
}
