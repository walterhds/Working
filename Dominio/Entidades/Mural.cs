using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Mural : EntidadeBase
    {
        public virtual string Comentario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
