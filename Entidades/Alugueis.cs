using Entidades.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Util;

namespace Entidades
{
    [Table("rent")]
    public class Alugueis
    {
        private int _horas;
        private decimal _precoTotal;
        private DateTime _dataCheckin;
        private DateTime _dataCheckout;

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("vehicle_id")]
        public Carros Carro { get; set; }
        [ForeignKey("client_id")]
        public Clientes Cliente { get; set; }
        [Column("date_in")]
        public DateTime DataCheckin
        {
            get
            { return _dataCheckin; }
            set
            {
                if (value == null)
                    throw new Exception("Data de Checkin não pode ser nula.");

                if (value < DateTime.Now)
                    throw new Exception("Data de Checkin tem que ser maior que agora.");

                _dataCheckin = value;
            }
        }
        [Column("date_out")]
        public DateTime DataCheckout
        {
            get
            { return _dataCheckout; }
            set
            {
                if (value == null)
                    throw new Exception("Data de Checkout não pode ser nula.");

                if (value < DateTime.Now)
                    throw new Exception("Data de Checkout tem que ser maior que agora.");

                if (value < DataCheckin)
                    throw new Exception("Data de Checkout não pode ser menor que data de Checkin.");

                _dataCheckout = value;
            }
        }
        [Column("hours")]
        public int Horas
        {
            get
            {
                this._horas = DataUtil.DiferencaEntreHoras(DataCheckin, DataCheckout);
                return _horas;
            }

            private set { _horas = value; }
        }
        [Column("price_at_day")]
        public decimal PrecoNoDia { get; set; }
        [Column("total_price")]
        public decimal PrecoTotal
        {
            get
            {
                this._precoTotal = PrecoNoDia * Horas;
                return _precoTotal;
            }

            private set { _precoTotal = value; }
        }
        [Column("date")]
        public DateTime DataAluguel { get; set; }
        [Column("source")]
        public string Origem { get; set; }

        public Alugueis()
        {

        }
    }
}
