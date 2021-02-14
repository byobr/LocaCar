using Entidades;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorios
{
    public class AlugueisRepositorio : IAlugueisRepositorio
    {
        private readonly Contexto _contexto;

        public AlugueisRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Alugueis Add(Alugueis Aluguel)
        {
            try
            {
                _contexto.Alugueis.Add(Aluguel);
                _contexto.SaveChanges();
                return _contexto.Alugueis.Include(i=> i.Carro.Modelo).FirstOrDefault(f=>f.Id == Aluguel.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Aluguel: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Alugueis GetById(int Aluguel)
        {
            try
            {
                return _contexto.Alugueis.Include(c => c.Cliente).Include(c => c.Carro.Modelo).FirstOrDefault(f => f.Id == Aluguel);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Aluguel pelo Id: " + Aluguel + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public List<Alugueis> GetByClientId(int Cliente)
        {
            try
            {
                return _contexto.Alugueis.Include(c=> c.Cliente).Include(c => c.Carro.Modelo).Include(m => m.Carro.Marca).Where(f => f.Cliente.Id == Cliente).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Aluguel pelo Cliente: " + Cliente + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(int Aluguel)
        {
            try
            {
                _contexto.Alugueis.Remove(_contexto.Alugueis.SingleOrDefault(f=>f.Id == Aluguel));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Aluguel: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
