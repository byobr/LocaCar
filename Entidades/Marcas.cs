using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("manufacturers")]
    public class Marcas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Nome { get; set; }
    }
}
