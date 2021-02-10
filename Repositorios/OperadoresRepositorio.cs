using Entidades;
using Fronteiras.Repositorios;
using System;
using System.Linq;
using Util.Ferramentas;

namespace Repositorios
{
    public class OperadoresRepositorio : IOperadoresRepositorio
    {
        private readonly Contexto _contexto;

        public OperadoresRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Operadores Add(Operadores Operador)
        {
            try
            {
                Operador.Senha = MD5Hash.GerarHashMd5(Operador.Senha);
                _contexto.Operadores.Add(Operador);
                _contexto.SaveChanges();
                return Operador;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Operador: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Operadores GetById(int Operador)
        {
            try
            {
                return _contexto.Operadores.FirstOrDefault(f => f.Id == Operador);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Operador: " + Operador + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Operadores GetByLogin(int Matricula) 
        {
            try
            {
                return _contexto.Operadores.FirstOrDefault(f => f.Matricula == Matricula);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Operador: " + Matricula + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(int Matricula)
        {
            try
            {
                _contexto.Operadores.Remove(_contexto.Operadores.SingleOrDefault(f=>f.Matricula == Matricula));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Operador: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
