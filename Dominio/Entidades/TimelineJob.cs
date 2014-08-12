using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TimelineJob:EntidadeBase
    {
        public virtual string Comentario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Job Job { get; set; }
    }
}
