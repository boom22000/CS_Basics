using System;
using System.ComponentModel.DataAnnotations;

namespace RealTimeEconomy.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurDateTime { get; set; }
        public decimal Price { get; set; }
        public double ExchangeRate { get; set; }
    }
}
