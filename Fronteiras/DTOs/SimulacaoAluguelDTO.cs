using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fronteiras.DTOs
{
    public class SimulacaoAluguelDTO
    {
        public virtual DateTime DateCheckin { get; set; }
        public virtual DateTime DateCheckout { get; set; }
        public virtual Carros Carro { get; set; }
        public virtual int Horas { get; set; }
        public virtual decimal PrecoTotal { get; set; }
    }
}
