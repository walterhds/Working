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
    public class TipoContaService
    {
        private readonly ITipoContaRepositorio _tipoContaRepositorio;

        public TipoContaService(ITipoContaRepositorio tipoContaRepositorio)
        {
            _tipoContaRepositorio = tipoContaRepositorio;
        }

        public bool Cadastrar(TipoConta tipoConta)
        {
            return _tipoContaRepositorio.Cadastrar(tipoConta);
        }

        public bool Remover(TipoConta tipoConta)
        {
            return _tipoContaRepositorio.Remover(tipoConta);
        }

        public TipoConta ObterPorId(int id)
        {
            return _tipoContaRepositorio.ObterPorId(id);
        }

        public IList<TipoConta> Listar(Expression<Func<TipoConta, bool>> filtro)
        {
            return _tipoContaRepositorio.Listar(filtro);
        } 
    }
}
