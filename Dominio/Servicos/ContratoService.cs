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
    public class ContratoService
    {
        private readonly IContratoRepositorio _contratoRepositorio;

        public ContratoService(IContratoRepositorio contratoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
        }

        public bool Cadastrar(Contrato contrato)
        {
            return _contratoRepositorio.Cadastrar(contrato);
        }

        public bool Remover(Contrato contrato)
        {
            return _contratoRepositorio.Remover(contrato);
        }

        public IList<Contrato> Listar(Expression<Func<Contrato, bool>> filtro)
        {
            return _contratoRepositorio.Listar(filtro);
        }

        public Contrato ObterPorId(int id)
        {
            return _contratoRepositorio.ObterPorId(id);
        }

        public Contrato ObterPorFiltro(Expression<Func<Contrato, bool>> filtro)
        {
            return _contratoRepositorio.ObterPorFiltro(filtro);
        }
    }
}
