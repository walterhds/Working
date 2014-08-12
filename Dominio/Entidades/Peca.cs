using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Peca:EntidadeBase
    {
        public virtual string Descricao { get; set; }
        public virtual IList<Fornecedor> Fornecedor { get; set; } 
    }
}
