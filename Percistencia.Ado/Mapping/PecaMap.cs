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
    public class PecaMap:ClassMapping<Peca>
    {
        public PecaMap()
        {
            this.Table("PECA");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(p => p.Descricao, m => m.Column("DESCRICAO"));
            Bag<Fornecedor>(p => p.Fornecedor, c =>
            {
                c.Table("PECAS_FORNECEDORES");
                c.Cascade(Cascade.None);
                c.Lazy(CollectionLazy.Lazy);
                c.Fetch(CollectionFetchMode.Join);
                c.Key(k => k.Column("ID_PECA"));
            }, map => map.ManyToMany(p => p.Column("ID_FORNECEDOR")));
        }
    }
}
