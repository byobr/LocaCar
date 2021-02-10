using Entidades;
using Fronteiras.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorios
{
    public class ModelosRepositorio : IModelosRepositorio
    {
        private readonly Contexto _contexto;

        public ModelosRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Modelos Add(Modelos Modelo)
        {
            try
            {
                _contexto.Modelos.Add(Modelo);
                _contexto.SaveChanges();
                return Modelo;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Modelo: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public List<Modelos> GetAll()
        {
            try
            {
                return _contexto.Modelos.Include(i=> i.Marca).ToList();

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter todas Modelos: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Modelos GetById(int Modelo)
        {
            try
            {
                return _contexto.Modelos.Include(i=>i.Marca).FirstOrDefault(f => f.Id == Modelo);

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter todas Modelos: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(int Modelo)
        {
            try
            {
                _contexto.Modelos.Remove(_contexto.Modelos.SingleOrDefault(f => f.Id == Modelo));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Modelo: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
