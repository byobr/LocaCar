using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Ferramentas;

namespace Entidades
{
    [Table("clients")]
    public class Clientes
    {
        private string _cpf { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("cellphone")]
        public string Celular { get; set; }
        [Column("document_number")]
        public string Cpf
        {
            get { return _cpf; }
            set
            {
                if (!ValidaCPF.ValidarCPF(value))
                {
                    throw new Exception("CPF Inválido");
                }
                else
                {
                    this._cpf = value;
                }
            }
        }
        [Column("password")]
        public string Senha { get; set; }
        [Column("birthday")]
        public DateTime Aniversaio { get; set; }
        [Column("cep")]
        public string Cep { get; set; }
        [Column("street_address")]
        public string Logradouro { get; set; }
        [Column("number")]
        public string Numero { get; set; }
        [Column("street_address2")]
        public string Complemento { get; set; }
        [Column("city")]
        public string Cidade { get; set; }
        [Column("state")]
        public string Estado { get; set; }
    }
}
