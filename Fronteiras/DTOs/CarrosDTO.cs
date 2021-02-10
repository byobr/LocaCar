using Entidades;
using Entidades.Enum;

namespace Fronteiras.DTOs
{
    public class CarrosDTO
    {
        public virtual string Placa { get; set; }
        public virtual int ModeloId { get; set; }
        public virtual int AnoModelo { get; set; }
        public virtual decimal ValorHora { get; set; }
        public virtual Combustiveis Combustivel { get; set; }
        public virtual int LimitePortaMalas { get; set; }
        public virtual Categorias Categoria { get; set; }
    }
}
