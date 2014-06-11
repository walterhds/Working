using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dominio.Servicos
{
    public class FuncionarioService
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioService(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public bool Cadastrar(Funcionario funcionario)
        {
            return _funcionarioRepositorio.Cadastrar(funcionario);
        }

        public IList<Funcionario> Listar(Expression<Func<Funcionario, bool>> filtro)
        {
            return _funcionarioRepositorio.Listar(filtro);
        }

        public Funcionario ObterPorId(int id)
        {
            return _funcionarioRepositorio.ObterPorId(id);
        }

        public bool Remover(Funcionario funcionario)
        {
            return _funcionarioRepositorio.Remover(funcionario);
        }

        public Funcionario ObterPorLogin(string login)
        {
            return _funcionarioRepositorio.ObterPorLogin(login);
        }
    }
}
