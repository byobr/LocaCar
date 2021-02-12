using BLL;
using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using LocaCar.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;
using Xunit;

namespace TestesUnitarios
{
    public abstract class BaseTestes
    {
        //Mock Marcas
        public static Mock<IMarcasBLL> _mockMarcasBLL = new Mock<IMarcasBLL>();
        public static Mock<IMarcasRepositorio> _mockMarcasRepositorio = new Mock<IMarcasRepositorio>();
        public static MarcasBLL MarcasBLL = new MarcasBLL(_mockMarcasRepositorio.Object);

        //Mock Auth
        public static Mock<IAutorizacaoBLL> _mockAutorizacaoBLL = new Mock<IAutorizacaoBLL>();

        public static Mock<IOperadoresRepositorio> _mockOperadoresRepositorio = new Mock<IOperadoresRepositorio>();
        public static Mock<IOperadoresBLL> _mockOperadoresBLL = new Mock<IOperadoresBLL>();
        public static OperadoresBLL OperadoresBLL = new OperadoresBLL(_mockOperadoresRepositorio.Object);
        public static Mock<IClientesRepositorio> _mockClientesRepositorio = new Mock<IClientesRepositorio>();
        public static Mock<IClientesBLL> _mockClientesBLL = new Mock<IClientesBLL>();
        public static ClientesBLL ClientesBLL = new ClientesBLL(_mockClientesRepositorio.Object);

        //Mock Modelos
        public static Mock<IModelosBLL> _mockModelosBLL = new Mock<IModelosBLL>();
        public static Mock<IModelosRepositorio> _mockModelosRepositorio = new Mock<IModelosRepositorio>();
        public static ModelosBLL ModelosBLL = new ModelosBLL(_mockModelosRepositorio.Object, _mockMarcasRepositorio.Object);


        //Controllers
        public static MarcasController _marcasController = new MarcasController(_mockMarcasBLL.Object);
        public static ModelosController _modelosController = new ModelosController(_mockModelosBLL.Object);
        public static AuthController _authController = new AuthController(_mockOperadoresBLL.Object, _mockClientesBLL.Object, _mockAutorizacaoBLL.Object);

        protected static void Configurar()
        {
            SetupMarcasController();
            SetupModelosController();
            SetupAuthController();
        }

        private static void SetupMarcasController()
        {
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

        }

        private static void SetupAuthController()
        {
            List<Clientes> Clientes = new List<Clientes>
            {
                new Clientes{Id = 1,
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
                },
                new Clientes{Id = 2,
                    Nome = "Maria",
                    Aniversaio = DateTime.Now,
                    Celular = "31988886666",
                    Cep = "30000-000",
                    Cidade = "BH",
                    Complemento = "TAL",
                    Cpf = "52760848078",
                    Email = "maria@locacar.com",
                    Estado = "MG",
                    Logradouro = "Rua Tal",
                    Numero = "123 Tal",
                    Senha = "698dc19d489c4e4db73e28a713eab07b" //teste
                },
            };

            List<Operadores> Operadores = new List<Operadores>
            {
                new Operadores{ Id = 1, Nome = "Operador1", Matricula = 123, Senha = "698dc19d489c4e4db73e28a713eab07b" /*teste*/ },
                new Operadores{ Id = 2, Nome = "Operador2", Matricula = 124, Senha = "698dc19d489c4e4db73e28a713eab07b" /*teste*/ }
            };

            _mockClientesBLL.Setup(m => m.ObterPorId(It.IsAny<int>())).Returns(ClientesBLL.ObterPorId(2));

            _mockClientesRepositorio.Setup(m =>
            m.GetById(It.IsAny<int>())).Returns(Clientes.FirstOrDefault(i=>i.Id == 2));

            _mockClientesRepositorio.Setup(m =>
            m.GetByCpf(It.IsAny<string>())).Returns(Clientes.FirstOrDefault(c => c.Cpf == "21483212009"));


            _mockOperadoresBLL.Setup(m =>
            m.ObterPorId(2)).Returns(OperadoresBLL.ObterPorId(2));

            _mockOperadoresRepositorio.Setup(m =>
            m.GetById(2)).Returns(Operadores.FirstOrDefault(i => i.Id == 2));

            _mockOperadoresRepositorio.Setup(m =>
            m.GetByLogin(123)).Returns(Operadores.FirstOrDefault(m => m.Matricula == 123));

        }

        private static void SetupModelosController()
        {
            var Modelos = new List<Modelos>
            {
                            new Modelos{Id = 1, Marca = new Marcas { Id = 1, Nome = "Fiat" },  Modelo = "Palio"},
                            new Modelos{Id = 2, Marca = new Marcas { Id = 1, Nome = "Fiat" }, Modelo = "Uno"}
            };

            _mockModelosRepositorio.Setup(m =>
            m.GetAll()).Returns(Modelos);

            _mockModelosBLL.Setup(m =>
            m.ObterTodos()).Returns(ModelosBLL.ObterTodos());
        }

        public static SaidaAPI RetornarSaidaApi(IActionResult ResultadoApi, TipoSaidaApiEnum SaidaObjeto)
        {
            SaidaAPI Dados = new SaidaAPI();

            switch (SaidaObjeto)
            {
                case TipoSaidaApiEnum.OK:
                    var ResultadoOK = Assert.IsType<OkObjectResult>(ResultadoApi);
                    Dados = Assert.IsType<SaidaAPI>(ResultadoOK.Value);
                    break;

                case TipoSaidaApiEnum.BADREQUEST:
                    var ResultadoBR = Assert.IsType<BadRequestObjectResult>(ResultadoApi);
                    Dados = Assert.IsType<SaidaAPI>(ResultadoBR.Value);
                    break;

                case TipoSaidaApiEnum.CREATED:
                    var ResultadoCreated = Assert.IsType<CreatedResult>(ResultadoApi);
                    Dados = Assert.IsType<SaidaAPI>(ResultadoCreated.Value);
                    break;

                case TipoSaidaApiEnum.NOTFOUND:
                    var ResultadoNF = Assert.IsType<NotFoundObjectResult>(ResultadoApi);
                    Dados = Assert.IsType<SaidaAPI>(ResultadoNF.Value);
                    break;
            }

            return Dados;
        }
    }
}
