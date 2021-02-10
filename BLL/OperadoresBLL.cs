using Entidades;
using Fronteiras.BLL;
using Fronteiras.DTOs;
using Fronteiras.Repositorios;
using System;
using Util;

namespace BLL
{
    public class OperadoresBLL : IOperadoresBLL
    {
        private readonly IOperadoresRepositorio _operadoresRepositorio;
        public OperadoresBLL(IOperadoresRepositorio operadoresRepositorio)
        {
            this._operadoresRepositorio = operadoresRepositorio;
        }

        public Operadores Adicionar(OperadoresDTO Operador)
        {
            Operadores Inserir = new Operadores
            {
                Matricula = Operador.Matricula,
                Senha = Operador.Senha,
                Nome = Operador.Nome
            };

            Operadores Retorno = this._operadoresRepositorio.Add(Inserir);

            return Retorno;
        }
        public Operadores ObterPorId(int Operador)
        {
            return this._operadoresRepositorio.GetById(Operador);
        }
        public Operadores ObterPorMatricula(int Matricula)
        {
            return this._operadoresRepositorio.GetByLogin(Matricula);
        }
        public bool Remover(int Operador)
        {
            return this._operadoresRepositorio.Remove(Operador);
        }
    }
}
