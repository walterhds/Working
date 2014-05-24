﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class JobMap : ClassMapping<Job>
    {
        public JobMap()
        {
            this.Table("JOB");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.Briefing,m=>m.Column("BRIEFING"));
            this.Property(p=>p.Pecas,m=>m.Column("PECAS"));
            this.Property(p=>p.Descricao,m=>m.Column("DESCRICAO"));
            this.ManyToOne(p=>p.Funcionario, m =>
            {
                m.Column("FUNCIONARIO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
            this.Property(p=>p.DataEstimativa,m=>m.Column("DATA_ESTIMATIVA"));
            this.Property(p=>p.DataEntrega,m=>m.Column("DATA_ENTREGA"));
            this.Property(p=>p.HorasNecessarias,m=>m.Column("HORAS_NECESSARIAS"));
            this.ManyToOne(p=>p.Cliente, m =>
            {
                m.Column("CLIENTE");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
