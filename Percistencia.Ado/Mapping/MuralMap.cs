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
    public class MuralMap : ClassMapping<Mural>
    {
        public MuralMap()
        {
            this.Table("MURAL");
            this.Id(e => e.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(e => e.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(e => e.Comentario, m => m.Column("COMENTARIO"));
            this.ManyToOne(e => e.Funcionario, m =>
            {
                m.Column("ID_FUNCIONARIO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
