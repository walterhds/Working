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
    public class AlteracaoContratoMap:ClassMapping<AlteracaoContrato>
    {
        public AlteracaoContratoMap()
        {
            this.Table("ALTERACAO_CONTRATO");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.NumeroAlteracao,m=>m.Column("NUMERO_ALTERACAO"));
            this.Property(p=>p.DataVencimento,m=>m.Column("DATA_VENCIMENTO"));
            this.Property(p=>p.Valor,m=>m.Column("VALOR"));
            this.Property(p=>p.NumeroParcelas,m=>m.Column("NUMERO_PARCELAS"));
            this.Property(p=>p.DataPrimeiraParcela,m=>m.Column("DATA_PRIMEIRA_PARCELA"));
            this.Property(p=>p.Descricao,m=>m.Column("DESCRICAO"));
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
