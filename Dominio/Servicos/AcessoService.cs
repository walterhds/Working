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
    public class AcessoService
    {
        private readonly IAcessoRepositorio _acessoRepositorio;

        public AcessoService(IAcessoRepositorio acessoRepositorio)
        {
            _acessoRepositorio = acessoRepositorio;
        }

        public bool Cadastrar(Acesso acesso)
        {
            return _acessoRepositorio.Cadastrar(acesso);
        }

        public bool Remover(Acesso acesso)
        {
            return _acessoRepositorio.Remover(acesso);
        }

        public Acesso ObterPorId(int id)
        {
            return _acessoRepositorio.ObterPorId(id);
        }

        public IList<Acesso> Listar(Expression<Func<Acesso, bool>> filtro)
        {
            return _acessoRepositorio.Listar(filtro);
        } 
    }
}
