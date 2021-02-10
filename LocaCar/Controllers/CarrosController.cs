using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace LocaCar.Controllers
{
    [Authorize(Roles = "Operador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly ICarrosBLL _carrosBLL;

        public CarrosController(ICarrosBLL carrosBLL)
        {
            this._carrosBLL = carrosBLL;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        public IActionResult CadastrarCarro(CarrosDTO Marca)
        {
            try
            {
                Carros Retorno = this._carrosBLL.Adicionar(Marca);

                return Created("api/Carros", new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        public IActionResult ObterTodosCarros()
        {
            try
            {
                List<Carros> retorno = this._carrosBLL.ObterTodos();

                return Ok(new SaidaAPI { ExecutadoComSucesso = true, Data = retorno });
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
        public IActionResult RemoverMarcas(int Carro)
        {
            try
            {
                if (_carrosBLL.ObterPorId(Carro) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Carro não encontrado com o Id: " + Carro });

                bool retorno = this._carrosBLL.Remover(Carro);

                return Ok(new SaidaAPI { ExecutadoComSucesso = retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }
    }
}
