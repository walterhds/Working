using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using NHibernate;
using NHibernate.Linq;

namespace Percistencia.Ado.Repositorio
{
    public class ClienteRepositorio : BaseRepositorio<Cliente>, IClienteRepositorio
    {
        public Cliente ObterPorLogin(string login)
        {
            return this.Sessao.Query<Cliente>().FirstOrDefault(e => e.Login == login);
        }
    }
}
