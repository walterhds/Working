using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ParcelasPagar:EntidadeBase
    {
        public virtual ContasPagar Conta { get; set; }
        public virtual int NumeroParcela { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual DateTime DataPagamento { get; set; }
        public virtual decimal ValorPago { get; set; }
        public virtual string Situacao { get; set; }
    }
}
