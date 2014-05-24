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
    public class ClienteMap : ClassMapping<Cliente>
    {
        public ClienteMap()
        {
            this.Table("CLIENTE");
            this.Id(p=>p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p=>p.Nome,m=>m.Column("NOME"));
            this.Property(p=>p.Telefone,m=>m.Column("TELEFONE"));
            this.Property(p=>p.Contato,m=>m.Column("CONTATO"));
            this.Property(p=>p.Celular,m=>m.Column("CELULAR"));
            this.Property(p=>p.Email,m=>m.Column("EMAIL"));
            this.Property(p=>p.Login,m=>m.Column("LOGIN"));
            this.Property(p=>p.Senha,m=>m.Column("SENHA"));
            this.Property(p=>p.DataRegistro,m=>m.Column("DATA_REGISTRO"));
            this.Property(p=>p.Permissao,m=>m.Column("PERMISSAO"));
        }
    }
}
