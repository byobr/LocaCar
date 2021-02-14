using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ModelosBLL : IModelosBLL
    {
        private readonly IModelosRepositorio _modelosRepositorio;
        private readonly IMarcasRepositorio _marcasRepositorio;
        public ModelosBLL(IModelosRepositorio modelosRepositorio, IMarcasRepositorio marcasRepositorio)
        {
            this._modelosRepositorio = modelosRepositorio;
            this._marcasRepositorio = marcasRepositorio;
        }

        public Modelos Adicionar(ModelosDTO Modelo)
        {
            Marcas Marca = _marcasRepositorio.GetById(Modelo.MarcaId);
            if (Marca == null)
                throw new Exception("Marca não encontrada: " + Modelo.MarcaId);

            Modelos Inserir = new Modelos { Marca = Marca, Modelo = Modelo.NomeModelo };

            Modelos Retorno = this._modelosRepositorio.Add(Inserir);

            return Retorno;
        }
        public List<ModelosDTO> ObterTodos()
        {
            var Retorno = new List<ModelosDTO>();

            foreach (Modelos Modelo in this._modelosRepositorio.GetAll())
            {
                Retorno.Add(new ModelosDTO { MarcaId = Modelo.Marca.Id, NomeModelo = Modelo.Modelo, ModeloId = Modelo.Id });
            }

            return Retorno;
        }

        public Modelos ObterPorId(int Modelo) 
        {
            return this._modelosRepositorio.GetById(Modelo);
        }

        public bool Remover(int Marca)
        {
            return this._modelosRepositorio.Remove(Marca);
        }
    }
}
