using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Job : EntidadeBase
    {
        public virtual string Briefing { get; set; }
        public virtual string Descricao { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime DataEstimativa { get; set; }
        public virtual DateTime DataEntrega { get; set; }
        public virtual string HorasNecessarias { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual SituacaoJob Situacao { get; set; }
        public virtual IList<Peca> Peca { get; set; } 
        public virtual IList<Fornecedor> Fornecedor { get; set; } 
        public virtual string Fase { get; set; }
        public virtual string Nome { get; set; }
        public virtual Contrato Contrato { get; set; }
    }
}
