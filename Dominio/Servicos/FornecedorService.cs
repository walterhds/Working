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
    public class FornecedorService
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public FornecedorService(IFornecedorRepositorio fornecedorRepositorio)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public bool Cadastrar(Fornecedor fornecedor)
        {
            return _fornecedorRepositorio.Cadastrar(fornecedor);
        }

        public IList<Fornecedor> Listar(Expression<Func<Fornecedor, bool>> filtro)
        {
            return _fornecedorRepositorio.Listar(filtro);
        }

        public Fornecedor ObterPorId(int id)
        {
            return _fornecedorRepositorio.ObterPorId(id);
        }

        public bool Remover(Fornecedor fornecedor)
        {
            return _fornecedorRepositorio.Remover(fornecedor);
        }
    }
}
