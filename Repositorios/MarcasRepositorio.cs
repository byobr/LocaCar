using Entidades;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorios
{
    public class MarcasRepositorio : IMarcasRepositorio
    {
        private readonly Contexto _contexto;

        public MarcasRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Marcas Add(Marcas Marca)
        {
            try
            {
                var teste =_contexto.Marcas.Add(Marca);
                _contexto.SaveChanges();
                return Marca;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Marca: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public List<Marcas> GetAll()
        {
            try
            {
                return _contexto.Marcas.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter todas Marcas: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Marcas GetById(int Modelo)
        {
            try
            {
                return _contexto.Marcas.FirstOrDefault(f => f.Id == Modelo);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Marca: " + Modelo + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(int Marca)
        {
            try
            {
                _contexto.Marcas.Remove(_contexto.Marcas.SingleOrDefault(f=>f.Id == Marca));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Marca: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
