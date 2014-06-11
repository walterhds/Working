using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dominio.Servicos
{
    public class CargoService
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public CargoService(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public bool Cadastrar(Cargo cargo)
        {
            return _cargoRepositorio.Cadastrar(cargo);
        }

        public IList<Cargo> Listar(Expression<Func<Cargo, bool>> filtro)
        {
            return _cargoRepositorio.Listar(filtro);
        }

        public Cargo ObterPorId(int id)
        {
            return _cargoRepositorio.ObterPorId(id);
        }

        public bool Remover(Cargo cargo)
        {
            return _cargoRepositorio.Remover(cargo);
        }
    }
}
