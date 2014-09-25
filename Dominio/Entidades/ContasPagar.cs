using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ContasPagar:EntidadeBase
    {
        public virtual TipoConta TipoConta { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual int NumeroParcelas { get; set; }
        public virtual DateTime DataPrimeiraParcela { get; set; }
        public virtual string Descricao { get; set; }
    }
}
