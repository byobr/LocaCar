using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using Util;

namespace BLL
{
    public class ClientesBLL : IClientesBLL
    {
        private readonly IClientesRepositorio _clientesRepositorio;
        public ClientesBLL(IClientesRepositorio clientesRepositorio)
        {
            this._clientesRepositorio = clientesRepositorio;
        }

        public Clientes Adicionar(ClientesDTO Cliente)
        {
            Clientes Inserir = new Clientes
            {
                Cpf = Cliente.Cpf,
                Senha = Cliente.Senha,
                Aniversaio = Cliente.Aniversaio,
                Cep = Cliente.Cep,
                Cidade = Cliente.Cidade,
                Complemento = Cliente.Complemento,
                Estado = Cliente.Estado,
                Logradouro = Cliente.Logradouro,
                Numero = Cliente.Numero,
                Celular = Cliente.Celular,
                Email = Cliente.Email,
                Nome = Cliente.Nome
            };
            Clientes Retorno = this._clientesRepositorio.Add(Inserir);
            return Retorno;
        }
        public Clientes ObterPorId(int Operador)
        {
            return this._clientesRepositorio.GetById(Operador);
        }
        public Clientes ObterPorCpf(string Cpf)
        {
            return this._clientesRepositorio.GetByCpf(Cpf);
        }
        public bool Remover(string Cpf)
        {
            return this._clientesRepositorio.Remove(Cpf);
        }
    }
}
