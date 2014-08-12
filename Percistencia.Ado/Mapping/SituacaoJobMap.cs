using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using NHibernate.Bytecode.CodeDom;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class SituacaoJobMap : ClassMapping<SituacaoJob>
    {
        public SituacaoJobMap()
        {
            this.Table("SITUACAO_JOB");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(p => p.Descricao, m => m.Column("DESCRICAO"));
            this.ManyToOne(p => p.IdAlterado, m =>
            {
                m.Column("ID_ALTERADO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
