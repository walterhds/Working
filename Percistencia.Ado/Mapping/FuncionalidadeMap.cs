﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Percistencia.Ado.Mapping
{
    public class FuncionalidadeMap : ClassMapping<Funcionalidade>
    {
        public FuncionalidadeMap()
        {
            this.Table("FUNCIONALIDADE");
            this.Id(p => p.Id, m =>
            {
                m.Column("ID");
                m.Generator(Generators.Identity);
            });
            this.Property(p => p.DataRegistro, m => m.Column("DATA_REGISTRO"));
            this.Property(p => p.Descricao, m => m.Column("DESCRICAO"));
        }
    }
}
