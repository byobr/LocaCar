using System.Text.Json.Serialization;

namespace Fronteiras.DTOs
{
    public class OperadoresDTO
    {
        public virtual int Matricula { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Senha { get; set; }
    }
}
