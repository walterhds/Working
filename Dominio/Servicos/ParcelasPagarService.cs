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
    public class ParcelasPagarService
    {
        private readonly IParcelasPagarRepositorio _parcelasPagarRepositorio;

        public ParcelasPagarService(IParcelasPagarRepositorio parcelasPagarRepositorio)
        {
            _parcelasPagarRepositorio = parcelasPagarRepositorio;
        }

        public bool Cadastrar(ParcelasPagar parcelasPagar)
        {
            return _parcelasPagarRepositorio.Cadastrar(parcelasPagar);
        }

        public bool Remover(ParcelasPagar parcelasPagar)
        {
            return _parcelasPagarRepositorio.Remover(parcelasPagar);
        }

        public ParcelasPagar ObterPorId(int id)
        {
            return _parcelasPagarRepositorio.ObterPorId(id);
        }

        public IList<ParcelasPagar> Listar(Expression<Func<ParcelasPagar, bool>> filtro)
        {
            return _parcelasPagarRepositorio.Listar(filtro);
        } 
    }
}
