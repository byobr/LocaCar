using Entidades;
using Fronteiras.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorios
{
    public class CarrosRepositorio : ICarrosRepositorio
    {
        private readonly Contexto _contexto;

        public CarrosRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Carros Add(Carros Carro)
        {
            try
            {
                _contexto.Carros.Add(Carro);
                _contexto.SaveChanges();
                return Carro;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Carro: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Carros GetById(int Carro)
        {
            try
            {
                return _contexto.Carros.FirstOrDefault(f => f.Id == Carro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter o Carro: " + Carro + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public List<Carros> GetAll()
        {
            try
            {
                return _contexto.Carros.Include(i => i.Modelo).Include(m => m.Marca).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter todos os Carros, " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(int Carro)
        {
            try
            {
                _contexto.Carros.Remove(_contexto.Carros.SingleOrDefault(s => s.Id == Carro));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Carro: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
