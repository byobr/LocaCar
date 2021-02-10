using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Ferramentas;

namespace Entidades
{
    [Table("users")]
    public class Operadores
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Nome { get; set; }
        [Column("login")]
        public int Matricula { get; set; }
        [Column("password")]
        public string Senha { get; set; }
    }
}
