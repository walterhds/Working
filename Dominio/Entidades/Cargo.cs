using System.Collections;
using System.Linq;
using Dominio.Entidades;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class Cargo : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual IList<Acesso> Acessos { get; set; }

        public virtual bool TemAcesso(string funcionalidade)
        {
            return Acessos.Any(a => a.Possui(funcionalidade));
        }
    }
}
