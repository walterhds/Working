using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using NHibernate;

namespace Percistencia.Ado.Repositorio
{
    public class JobRepositorio : BaseRepositorio<Job>, IJobRepositorio
    {
        
    }
}
