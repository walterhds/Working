using System;

namespace Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataRegistro { get; set; }
    }
}
