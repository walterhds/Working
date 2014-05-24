using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Dominio.Servicos
{
    public class ClienteService
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteService(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public void Cadastrar(Cliente cliente)
        {
            _clienteRepositorio.Cadastrar(cliente);
        }

        public void Remover(Cliente cliente)
        {
            _clienteRepositorio.Remover(cliente);
        }

        public IList<Cliente> Listar(Expression<Func<Cliente, bool>> filtro)
        {
            return _clienteRepositorio.Listar(filtro);
        }

        public Cliente ObterPorId(int id)
        {
            return _clienteRepositorio.ObterPorId(id);
        }

        public Cliente ObterPorLogin(string login)
        {
            return _clienteRepositorio.ObterPorLogin(login);
        }
    }
}
