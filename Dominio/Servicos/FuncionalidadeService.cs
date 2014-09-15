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
    public class FuncionalidadeService
    {
        private readonly IFuncionalidadeRepositorio _funcionalidadeRepositorio;

        public FuncionalidadeService(IFuncionalidadeRepositorio funcionalidadeRepositorio)
        {
            _funcionalidadeRepositorio = funcionalidadeRepositorio;
        }

        public bool Cadastrar(Funcionalidade funcionalidade)
        {
            return _funcionalidadeRepositorio.Cadastrar(funcionalidade);
        }

        public bool Remover(Funcionalidade funcionalidade)
        {
            return _funcionalidadeRepositorio.Remover(funcionalidade);
        }

        public Funcionalidade ObterPorId(int id)
        {
            return _funcionalidadeRepositorio.ObterPorId(id);
        }

        public IList<Funcionalidade> Listar(Expression<Func<Funcionalidade, bool>>  filtro)
        {
            return _funcionalidadeRepositorio.Listar(filtro);
        } 
    }
}
