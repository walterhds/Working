using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class CargoMap : ClassMapping<Cargo>
    {
        public CargoMap()
        {
            this.Table("CARGO");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.Nome, m => m.Column("NOME"));
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            Bag<Acesso>(p=>p.Acessos, c =>
            {
                c.Table("CARGO_ACESSO");
                c.Cascade(Cascade.None);
                c.Lazy(CollectionLazy.Lazy);
                c.Fetch(CollectionFetchMode.Join);
                c.Key(k=>k.Column("ID_CARGO"));
            },map=>map.ManyToMany(p=>p.Column("ID_ACESSO")));
        }
    }
}
