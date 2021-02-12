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
    [Authorize(Roles = "Operador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IModelosBLL _modelosBLL;
        public ModelosController(IModelosBLL modelosBLL)
        {
            this._modelosBLL = modelosBLL;
        }

        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult CadastrarModelo(ModelosDTO Modelo)
        {
            try
            {
                var Retorno = this._modelosBLL.Adicionar(Modelo);

                return Created("api/Modelos/" ,new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno } );
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult ObterTodosModelos()
        {
            try
            {
                List<ModelosDTO> Retorno = this._modelosBLL.ObterTodos();

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
        public IActionResult RemoverModelo(int Modelo)
        {
            try
            {
                if (_modelosBLL.ObterPorId(Modelo) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Modelo não encontrado com o Id: " + Modelo });

                bool retorno = this._modelosBLL.Remover(Modelo);

                return Ok(new SaidaAPI { ExecutadoComSucesso = retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }
    }
}
