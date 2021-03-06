﻿using Dominio.Entidades;
using System;

namespace Dominio.Repositorios
{
    public interface IBaseRepositorio<T> where T : EntidadeBase
    {
        bool Cadastrar(T entidade);
        System.Collections.Generic.IList<T> Listar(System.Linq.Expressions.Expression<Func<T, bool>> filtro);
        T ObterPorId(int id);
        bool Remover(T entidade);
        T ObterPorFiltro(System.Linq.Expressions.Expression<Func<T, bool>> filtro);
    }
}
