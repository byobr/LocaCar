using System;
using System.Text.Json.Serialization;

namespace Fronteiras.DTOs
{
    public class AlugueisDTO : SimularAlugueisDTO
    {
        public virtual int ClienteId { get; set; }
    }
}
