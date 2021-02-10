using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fronteiras.DTOs
{
    [Table("clients")]
    public class ClientesDTO
    {
        public virtual string Cpf { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Senha { get; set; }
        public virtual DateTime Aniversaio { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Estado { get; set; }
    }
}
