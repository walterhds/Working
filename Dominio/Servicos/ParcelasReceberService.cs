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
    public class ParcelasReceberService
    {
        private readonly IParcelasReceberRepositorio _parcelasReceberRepositorio;

        public ParcelasReceberService(IParcelasReceberRepositorio parcelasReceberRepositorio)
        {
            _parcelasReceberRepositorio = parcelasReceberRepositorio;
        }

        public bool Cadastrar(ParcelasReceber parcelasReceber)
        {
            return _parcelasReceberRepositorio.Cadastrar(parcelasReceber);
        }

        public bool Remover(ParcelasReceber parcelasReceber)
        {
            return _parcelasReceberRepositorio.Remover(parcelasReceber);
        }

        public ParcelasReceber ObterPorId(int id)
        {
            return _parcelasReceberRepositorio.ObterPorId(id);
        }

        public IList<ParcelasReceber> Listar(Expression<Func<ParcelasReceber, bool>> filtro)
        {
            return _parcelasReceberRepositorio.Listar(filtro);
        }
    }
}
