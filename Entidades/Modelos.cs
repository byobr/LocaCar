using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("models")]
    public class Modelos
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("model_name")]
        public string Modelo { get; set; }
        [ForeignKey("manufacturer")]
        public virtual Marcas Marca { get; set; }

    }
}
