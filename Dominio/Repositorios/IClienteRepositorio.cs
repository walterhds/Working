using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IClienteRepositorio : IBaseRepositorio<Cliente>
    {
        Cliente ObterPorLogin(string login);
    }
}
