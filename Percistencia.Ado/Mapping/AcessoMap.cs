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
    public class AcessoMap : ClassMapping<Acesso>
    {
        public AcessoMap()
        {
            this.Table("ACESSO");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(p=>p.Descricao,m=>m.Column("DESCRICAO"));
            Bag<Funcionalidade>(p=>p.Funcionalidade, c =>
            {
                c.Table("FUNCIONALIDADE_ACESSO");
                c.Lazy(CollectionLazy.Lazy);
                c.Fetch(CollectionFetchMode.Join);
                c.Cascade(Cascade.None);
                c.Key(k=>k.Column("ID_ACESSO"));
            },map=>map.ManyToMany(p=>p.Column("ID_FUNCIONALIDADE")));
        }
    }
}
