using System.Security.Cryptography.X509Certificates;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IFuncionarioRepositorio : IBaseRepositorio<Funcionario>
    {
        Funcionario ObterPorLogin(string login);
    }
}
