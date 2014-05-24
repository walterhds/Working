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
            this.Property(p => p.Permissao, m => m.Column("PERMISSAO"));
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
        }
    }
}
