using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Util;

namespace LocaCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOperadoresBLL _operadoresBLL;
        private readonly IClientesBLL _clientesBLL;
        private readonly IAutorizacaoBLL _autorizacaoBLL;

        public AuthController(
            IOperadoresBLL operadoresBLL,
            IClientesBLL clientesBLL,
            IAutorizacaoBLL autorizacaoBLL
            )
        {
            this._operadoresBLL = operadoresBLL;
            this._clientesBLL = clientesBLL;
            this._autorizacaoBLL = autorizacaoBLL;
        }

        //Rota de Login
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        public IActionResult Logar(LoginDTO login)
        {
            try
            {

                TokenDTO Retorno = _autorizacaoBLL.Login(login);

                return Ok( new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });

            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }


        //Operaçoes de Operadores
        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Route("Operador")]
        [Authorize(Roles = "Operador")]
        public IActionResult CadastrarOperador(OperadoresDTO Operador)
        {
            try
            {
                if(_operadoresBLL.ObterPorMatricula(Operador.Matricula) != null)
                    return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Matrícula já existe na base de dados." });

                Operadores Retorno = _operadoresBLL.Adicionar(Operador);

                return Created("api/Auth/operador?Matricula=" + Operador.Matricula, new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });

            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Route("Operador")]
        [Authorize(Roles = "Operador")]
        public IActionResult ObterPorMatricula(int Matricula)
        {
            try
            {
                Operadores Retorno = _operadoresBLL.ObterPorMatricula(Matricula);

                if(Retorno == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Operador não encontrado com Matrícula: " + Matricula });

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });

            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Route("Operador")]
        [Authorize(Roles = "Operador")]
        public IActionResult RemoverOperador(int Matricula)
        {
            try
            {
                if (_operadoresBLL.ObterPorMatricula(Matricula) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Operador não encontrado com Matrícula: " + Matricula });

                _operadoresBLL.Remover(Matricula);

                return Ok(new SaidaAPI { ExecutadoComSucesso = true });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        //Operações de Clientes
        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Route("Clientes")]
        public IActionResult CadastrarCliente(ClientesDTO Cliente)
        {
            try
            {
                if (_clientesBLL.ObterPorCpf(Cliente.Cpf) != null)
                    return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Cpf já existe na base de dados." });

                Clientes Retorno = _clientesBLL.Adicionar(Cliente);

                return Created("api/Auth/Clientes?Cpf=" + Cliente.Cpf, new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });

            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Route("Clientes")]
        [Authorize]
        public IActionResult ObterPorCpf(string Cpf)
        {
            try
            {
                Clientes Retorno = _clientesBLL.ObterPorCpf(Cpf);

                if (Retorno == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Cliente não encontrado com CPF: " + Cpf });

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });

            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Operador")]
        [Authorize]
        public IActionResult RemoverCliente(string Cpf)
        {
            try
            {
                if (_clientesBLL.ObterPorCpf(Cpf) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Cliente não encontrado com CPF: " + Cpf });

                _clientesBLL.Remover(Cpf);

                return Ok(new SaidaAPI { ExecutadoComSucesso = true });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }
    }
}
