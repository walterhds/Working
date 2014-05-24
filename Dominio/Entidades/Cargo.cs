using Dominio.Entidades;

namespace Dominio.Entidades
{
    public class Cargo : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual int Permissao { get; set; }
    }
}
