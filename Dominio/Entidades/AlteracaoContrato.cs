using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class AlteracaoContrato :EntidadeBase
    {
        public virtual Contrato Contrato { get; set; }
        public virtual int NumeroAlteracao { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual Decimal Valor { get; set; }
        public virtual int NumeroParcelas { get; set; }
        public virtual DateTime DataPrimeiraParcela { get; set; }
        public virtual string Descricao { get; set; }
    }
}
