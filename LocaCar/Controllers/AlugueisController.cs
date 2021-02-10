using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Util;

namespace LocaCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private readonly IAlugueisBLL _alugueisBLL;

        public AlugueisController(IAlugueisBLL alugueisBLL)
        {
            this._alugueisBLL = alugueisBLL;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [Route("SimularAluguel")]
        [AllowAnonymous]
        public IActionResult SimularAluguel(SimularAlugueisDTO Aluguel)
        {
            try
            {
                SimulacaoAluguelDTO Retorno = this._alugueisBLL.Simular(Aluguel);

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [Route("Cliente")]
        [Authorize]
        public IActionResult CadastrarAluguel(AlugueisDTO Aluguel)
        {
            try
            {
                Alugueis Retorno = this._alugueisBLL.Alugar(Aluguel);

                return Created("api/Alugueis/ObterAluguel?Aluguel=" + Retorno.Id, new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize(Roles = "Operador")]
        public IActionResult ObterAluguel(int Aluguel)
        {
            try
            {
                Alugueis Retorno = _alugueisBLL.ObterPorId(Aluguel);

                if (Retorno == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Aluguel não encontrado com o Id: " + Aluguel });

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize(Roles = "Operador,Cliente")]
        [Route("Cliente")]
        public IActionResult ObterAluguelPorCliente(int Cliente)
        {
            try
            {
                List<Alugueis> Retorno = _alugueisBLL.ObterPorCliente(Cliente);

                if (Retorno == null || Retorno.Count < 1)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Aluguéis não encontrados com o Id do Cliente: " + Cliente });

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Authorize(Roles = "Operador")]
        public IActionResult RemoverAluguel(int Aluguel)
        {
            try
            {
                if (_alugueisBLL.ObterPorId(Aluguel) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Aluguel não encontrado com o Id: " + Aluguel });

                bool retorno = this._alugueisBLL.Remover(Aluguel);

                return Ok(new SaidaAPI { ExecutadoComSucesso = retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }
    }
}
