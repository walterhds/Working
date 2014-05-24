
namespace Dominio.Entidades
{
    public class Funcionario : EntidadeBase
    {  
        public virtual string Nome { get; set; }
        public virtual int Telefone { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Login { get; set; }
        public virtual Situacao Situacao { get; set; }
        //
        public virtual string Cpf { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Email { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string QuantidadeHora { get; set; }
    }
}
