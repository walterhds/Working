using System.Collections;
using Dominio.Entidades;

namespace Dominio.Entidades
{
    public class Cliente:EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Contato { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Email { get; set; }
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        public virtual int Permissao { get; set; }
    }
}
