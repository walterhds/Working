using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using System.Linq.Expressions;

namespace Dominio.Servicos
{
    public class PecaService
    {
        private readonly IPecaRepositorio _pecaRepositorio;

        public PecaService(IPecaRepositorio pecaRepositorio)
        {
            _pecaRepositorio = pecaRepositorio;
        }

        public bool Cadastrar(Peca peca)
        {
            return _pecaRepositorio.Cadastrar(peca);
        }

        public IList<Peca> Listar(Expression<Func<Peca, bool>> filtro)
        {
            return _pecaRepositorio.Listar(filtro);
        }

        public void Remover(Peca peca)
        {
            _pecaRepositorio.Remover(peca);
        }

        public Peca ObterPorId(int id)
        {
            return _pecaRepositorio.ObterPorId(id);
        }
    }
}
