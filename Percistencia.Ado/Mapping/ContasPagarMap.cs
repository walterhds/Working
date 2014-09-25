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
    public class ContasPagarMap:ClassMapping<ContasPagar>
    {
        public ContasPagarMap()
        {
            this.Table("CONTAS_PAGAR");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.ManyToOne(p=>p.TipoConta, m =>
            {
                m.Column("TIPO_CONTA");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
            this.Property(p=>p.Valor,m=>m.Column("VALOR"));
            this.Property(p=>p.NumeroParcelas,m=>m.Column("NUMERO_PARCELAS"));
            this.Property(p=>p.DataPrimeiraParcela,m=>m.Column("DATA_PRIMEIRA_PARCELA"));
            this.Property(p=>p.Descricao,m=>m.Column("DESCRICAO"));
        }
    }
}
