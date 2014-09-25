using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Working.Models
{
    public class ObjetoParcela
    {
        public int Id { get; set; }
        public string DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}