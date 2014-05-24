using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class FuncionarioMap : ClassMapping<Funcionario>
    {
        public FuncionarioMap()
        {
            this.Table("FUNCIONARIO");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.Nome, m => m.Column("NOME"));
            this.Property(p => p.Telefone, m => m.Column("TELEFONE"));
            this.Property(p => p.Login, m => m.Column("LOGIN"));
            this.Property(p => p.Senha, m => m.Column("SENHA"));
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(p => p.Situacao, m => m.Column("SITUACAO"));
            this.Property(p => p.Celular, m => m.Column("CELULAR"));
            this.Property(p => p.Cpf, m => m.Column("CPF"));
            this.Property(p => p.Email, m => m.Column("EMAIL"));
            this.Property(p => p.Endereco, m => m.Column("ENDERECO"));
            this.Property(p => p.QuantidadeHora, m => m.Column("QUANTIDADEHORA"));
            this.ManyToOne(p => p.Cargo, m =>
            {
                m.Column("CARGO");
                m.Fetch(FetchKind.Join);
                m.Lazy(LazyRelation.Proxy);
                m.Cascade(Cascade.None);
            });
        }
    }
}
