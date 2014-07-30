using System.Linq;
using Dominio.Entidades;
using Dominio.Repositorios;
using NHibernate;
using NHibernate.Linq;

namespace Percistencia.Ado.Repositorio
{
    public class FuncionarioRepositorio : BaseRepositorio<Funcionario>, IFuncionarioRepositorio
    {
        public Funcionario ObterPorLogin(string login)
        {
            return this.Sessao.Query<Funcionario>().FirstOrDefault(e => e.Login.ToLower() == login.ToLower());
        }
    }
}
