using Entidades;
using Fronteiras.Repositorios;
using System;
using System.Linq;
using Util.Ferramentas;

namespace Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly Contexto _contexto;

        public ClientesRepositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }
        public Clientes Add(Clientes Cliente)
        {
            try
            {
                Cliente.Senha = MD5Hash.GerarHashMd5(Cliente.Senha);
                _contexto.Clientes.Add(Cliente);
                _contexto.SaveChanges();
                return Cliente;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir Cliente: " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Clientes GetById(int Cliente)
        {
            try
            {
                return _contexto.Clientes.FirstOrDefault(f => f.Id == Cliente);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Cliente: " + Cliente + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public Clientes GetByCpf(string Cpf)
        {
            try
            {
                return _contexto.Clientes.FirstOrDefault(f => f.Cpf == Cpf);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter a Cliente: " + Cpf + ", " + e.Message + " (" + e.InnerException + ")");
            }
        }

        public bool Remove(string Cpf)
        {
            try
            {
                _contexto.Clientes.Remove(_contexto.Clientes.SingleOrDefault(f=>f.Cpf == Cpf));
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao remover Cliente: " + e.Message + " (" + e.InnerException + ")");
            }
        }
    }
}
