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
    public class ParcelasPagarMa:ClassMapping<ParcelasPagar>
    {
        public ParcelasPagarMa()
        {
            this.Table("PARCELAS_PAGAR");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.ManyToOne(p=>p.Conta, m =>
            {
                m.Column("CONTA");
                m.Fetch(FetchKind.Join);
                m.Lazy(LazyRelation.Proxy);
                m.Cascade(Cascade.None);
            });
            this.Property(p=>p.NumeroParcela,m=>m.Column("NUMERO_PARCELA"));
            this.Property(p=>p.Valor,m=>m.Column("VALOR"));
            this.Property(p=>p.DataVencimento,m=>m.Column("DATA_VENCIMENTO"));
            this.Property(p=>p.DataPagamento,m=>m.Column("DATA_PAGA"));
            this.Property(p=>p.ValorPago,m=>m.Column("VALOR_PAGO"));
            this.Property(p=>p.Situacao,m=>m.Column("SITUACAO"));
        }
    }
}
