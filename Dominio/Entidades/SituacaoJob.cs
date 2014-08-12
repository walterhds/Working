using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class SituacaoJob : EntidadeBase
    {
        public virtual Funcionario IdAlterado { get; set; }
        public virtual string Descricao { get; set; }
    }
}
