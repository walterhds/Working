using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ParcelasReceber:EntidadeBase
    {
        public virtual int NumeroParcela { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual DateTime DataRecebida { get; set; }
        public virtual decimal ValorRecebido { get; set; }
        public virtual string Situacao { get; set; }
    }
}
