using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Compromisso:EntidadeBase
    {
        public virtual DateTime Data { get; set; }
        public virtual string Hora { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int Situacao { get; set; }
    }
}
