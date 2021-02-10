using Entidades.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades
{
    [Table("vehicles")]
    public class Carros
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("plate")]
        public string Placa { get; set; }
        [ForeignKey("manufacturer_id")]
        [JsonIgnore]
        public Marcas Marca { get; set; }
        [ForeignKey("model_id")]
        public Modelos Modelo { get; set; }
        [Column("year_made")]
        public int AnoModelo { get; set; }
        [Column("hourly_value")]
        public decimal ValorHora { get; set; }
        [Column("gas_type")]
        public Combustiveis Combustivel { get; set; }
        [Column("trunk_capacity")]
        public int LimitePortaMalas { get; set; }
        [Column("category")]
        public Categorias Categoria { get; set; }
    }
}
