using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Contrato : EntidadeBase
    {
        public virtual DateTime DataContrato { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual int NumeroParcelas { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual int NumeroContrato { get; set; }
        public virtual DateTime DataPrimeiraParcela { get; set; }
    }
}
