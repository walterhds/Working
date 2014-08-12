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
    public class MuralService
    {
        private readonly IMuralRepositorio _muralRepositorio;

        public MuralService(IMuralRepositorio muralRepositorio)
        {
            _muralRepositorio = muralRepositorio;
        }

        public bool Cadastrar(Mural mural)
        {
            return _muralRepositorio.Cadastrar(mural);
        }

        public bool Remover(Mural mural)
        {
            return _muralRepositorio.Remover(mural);
        }

        public IList<Mural> Listar(Expression<Func<Mural, bool>> filtro)
        {
            return _muralRepositorio.Listar(filtro);
        }

        public Mural ObterPorId(int id)
        {
            return _muralRepositorio.ObterPorId(id);
        }
    }
}
