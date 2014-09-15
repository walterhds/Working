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
    public class ContratoMap:ClassMapping<Contrato>
    {
        public ContratoMap()
        {
            this.Table("CONTRATO");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.DataContrato,m=>m.Column("DATA_CONTRATO"));
            this.Property(p=>p.DataVencimento,m=>m.Column("DATA_VENCIMENTO"));
            this.Property(p=>p.Descricao,m=>m.Column("DESCRICAO"));
            this.Property(p=>p.Valor,m=>m.Column("VALOR"));
            this.Property(p=>p.NumeroParcelas,m=>m.Column("NUMERO_PARCELAS"));
            this.Property(p=>p.NumeroContrato,m=>m.Column("NUMERO_CONTRATO"));
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
