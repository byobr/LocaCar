using System;
using System.Text.Json.Serialization;

namespace Fronteiras.DTOs
{
    public class SimularAlugueisDTO
    {
        public virtual int CarroId { get; set; }
        public virtual DateTime DataCheckin { get; set; }
        public virtual DateTime DataCheckout { get; set; }
        public virtual DateTime DataAluguel { get; set; }
        public virtual string Origem { get; set; }
    }
}
