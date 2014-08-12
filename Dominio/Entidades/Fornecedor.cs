using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Fornecedor : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Contato { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Email { get; set; }
    }
}
