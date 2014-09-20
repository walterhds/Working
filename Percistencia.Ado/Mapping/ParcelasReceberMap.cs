using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class ParcelasReceberMap:ClassMapping<ParcelasReceber>
    {
        public ParcelasReceberMap()
        {
            this.Table("PARCELAS_RECEBER");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.NumeroParcela,m=>m.Column("NUMERO_PARCELA"));
            this.Property(p=>p.Valor,m=>m.Column("VALOR"));
            this.Property(p=>p.DataVencimento,m=>m.Column("DATA_VENCIMENTO"));
            this.Property(p=>p.DataRecebida,m=>m.Column("DATA_RECEBIDA"));
            this.Property(p=>p.ValorRecebido,m=>m.Column("VALOR_RECEBIDO"));
            this.Property(p=>p.Situacao,m=>m.Column("SITUACAO"));
            this.ManyToOne(p=>p.Contrato, m =>
            {
                m.Column("CONTRATO");
                m.Lazy(LazyRelation.Proxy);
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
            });
        }
    }
}
