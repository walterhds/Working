using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class CompromissoMap : ClassMapping<Compromisso>
    {
        public CompromissoMap()
        {
            this.Table("COMPROMISSO");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro, m => m.Column("DATAREGISTRO"));
            this.Property(p => p.Data, m => m.Column("DATA"));
            this.Property(p => p.Hora, m => m.Column("HORA"));
            this.Property(p => p.Descricao, m => m.Column("DESCRICAO"));
            this.Property(p => p.Situacao, m => m.Column("SITUACAO"));
            this.ManyToOne(p => p.UltimoFuncionario, m =>
            {
                m.Column("ID_ALTERADO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
