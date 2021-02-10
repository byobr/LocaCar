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
    public class MarcasController : ControllerBase
    {
        private readonly IMarcasBLL _marcasBLL;

        public MarcasController(IMarcasBLL marcasBLL)
        {
            this._marcasBLL = marcasBLL;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        public IActionResult CadastrarMarca(MarcasDTO Marca)
        {
            try
            {
                Marcas Retorno = this._marcasBLL.Adicionar(Marca);

                return Created("api/Marcas", new SaidaAPI { ExecutadoComSucesso = true, Data = Retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaidaAPI), StatusCodes.Status400BadRequest)]
        public IActionResult ObterTodasMarcas()
        {
            try
            {
                List<Marcas> retorno = this._marcasBLL.ObterTodos();

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
        public IActionResult RemoverMarcas(int Marca)
        {
            try
            {
                if (_marcasBLL.ObterTodos().Where(w=> w.Id == Marca) == null)
                    return NotFound(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = "Marca não encontrado com o Id: " + Marca });

                bool retorno = this._marcasBLL.Remover(Marca);

                return Ok(new SaidaAPI { ExecutadoComSucesso = retorno });
            }
            catch (Exception e)
            {
                return BadRequest(new SaidaAPI { ExecutadoComSucesso = false, Mensagem = e.Message });
            }
        }
    }
}
