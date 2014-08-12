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
    public class SituacaoJobService
    {
        private readonly ISituacaoJobRepositorio _situacaoJobRepositorio;

        public SituacaoJobService(ISituacaoJobRepositorio situacaoJobRepositorio)
        {
            _situacaoJobRepositorio = situacaoJobRepositorio;
        }

        public bool Cadastrar(SituacaoJob situacaoJob)
        {
            return _situacaoJobRepositorio.Cadastrar(situacaoJob);
        }

        public IList<SituacaoJob> Listar(Expression<Func<SituacaoJob, bool>> filtro)
        {
            return _situacaoJobRepositorio.Listar(filtro);
        }

        public bool Remover(SituacaoJob situacaoJob)
        {
            return _situacaoJobRepositorio.Remover(situacaoJob);
        }

        public SituacaoJob ObterPorId(int id)
        {
            return _situacaoJobRepositorio.ObterPorId(id);
        }
    }
}
