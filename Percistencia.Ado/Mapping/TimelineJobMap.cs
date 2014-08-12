using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class TimelineJobMap:ClassMapping<TimelineJob>
    {
        public TimelineJobMap()
        {
            this.Table("TIMELINE_JOB");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.Comentario,m=>m.Column("COMENTARIO"));
            this.ManyToOne(p=>p.Funcionario, m =>
            {
                m.Column("ID_FUNCIONARIO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
            this.ManyToOne(p=>p.Job, m =>
            {
                m.Column("ID_JOB");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
