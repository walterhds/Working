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
    public class ContasPagarService
    {
        private readonly IContasPagarRepositorio _contasPagarRepositorio;

        public ContasPagarService(IContasPagarRepositorio contasPagarRepositorio)
        {
            _contasPagarRepositorio = contasPagarRepositorio;
        }

        public bool Cadastrar(ContasPagar contasPagar)
        {
            return _contasPagarRepositorio.Cadastrar(contasPagar);
        }

        public bool Remover(ContasPagar contasPagar)
        {
            return _contasPagarRepositorio.Remover(contasPagar);
        }

        public ContasPagar ObterPorId(int id)
        {
            return _contasPagarRepositorio.ObterPorId(id);
        }

        public ContasPagar ObterPorFiltro(Expression<Func<ContasPagar, bool>> filtro)
        {
            return _contasPagarRepositorio.ObterPorFiltro(filtro);
        }

        public IList<ContasPagar> Listar(Expression<Func<ContasPagar, bool>> filtro)
        {
            return _contasPagarRepositorio.Listar(filtro);
        }
    }
}
