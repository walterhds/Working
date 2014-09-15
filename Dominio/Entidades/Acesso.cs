using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Acesso : EntidadeBase
    {
        public virtual IList<Funcionalidade> Funcionalidade { get; set; }
        public virtual string Descricao { get; set; }

        public virtual bool Possui(string funcionalidade)
        {
            return Funcionalidade.Any(p => p.Descricao == funcionalidade);
        }
    }
}
