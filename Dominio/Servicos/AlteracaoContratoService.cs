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
    public class AlteracaoContratoService
    {
        private readonly IAlteracaoContratoRepositorio _alteracaoContratoRepositorio;

        public AlteracaoContratoService(IAlteracaoContratoRepositorio alteracaoContratoRepositorio)
        {
            _alteracaoContratoRepositorio = alteracaoContratoRepositorio;
        }

        public bool Cadastrar(AlteracaoContrato alteracaoContrato)
        {
            return _alteracaoContratoRepositorio.Cadastrar(alteracaoContrato);
        }

        public bool Remover(AlteracaoContrato alteracaoContrato)
        {
            return _alteracaoContratoRepositorio.Remover(alteracaoContrato);
        }

        public AlteracaoContrato ObterPorId(int id)
        {
            return _alteracaoContratoRepositorio.ObterPorId(id);
        }

        public IList<AlteracaoContrato> Listar(Expression<Func<AlteracaoContrato, bool>> filtro)
        {
            return _alteracaoContratoRepositorio.Listar(filtro);
        }  
    }
}
