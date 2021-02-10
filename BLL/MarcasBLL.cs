using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System.Collections.Generic;

namespace BLL
{
    public class MarcasBLL : IMarcasBLL
    {
        private readonly IMarcasRepositorio _marcasRepositorio;
        public MarcasBLL(IMarcasRepositorio marcasRepositorio)
        {
            this._marcasRepositorio = marcasRepositorio;
        }

        public Marcas Adicionar(MarcasDTO Marca)
        {
            Marcas Inserir = new Marcas { Nome = Marca.Marca };
            Marcas Retorno = this._marcasRepositorio.Add(Inserir);
            return Retorno;
        }
        public List<Marcas> ObterTodos()
        {
            return this._marcasRepositorio.GetAll();
        }
        public bool Remover(int Marca)
        {
            return this._marcasRepositorio.Remove(Marca);
        }
    }
}
