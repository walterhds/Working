using Dominio.Entidades;
using Dominio.Repositorios;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Percistencia.Ado
{
    public abstract class BaseRepositorio<T> : IBaseRepositorio<T> where T : EntidadeBase
    {
        protected ISession Sessao
        {
            get { return NHibernateHelper.GetCurrentSession(); }
        }

        //Cadastrar ou alterar
        public bool Cadastrar(T entidade)
        {
            try
            {
                this.Sessao.Merge(entidade);
                return true;
            }
            catch (Exception e)
            {
                this.Sessao.Transaction.Rollback();
                return false;
            }
        }

        public IList<T> Listar(Expression<Func<T, bool>> filtro)
        {
            return this.Sessao.Query<T>().Where(filtro).ToList();
        }

        public T ObterPorId(int id)
        {
            return this.Sessao.Query<T>().FirstOrDefault(e => e.Id == id);
        }

        public bool Remover(T entidade)
        {
            try
            {
                this.Sessao.Delete(entidade);
                return true;
            }
            catch (Exception e)
            {
                this.Sessao.Transaction.Rollback();
                return false;
            }
        }

        public T ObterPorFiltro(Expression<Func<T, bool>> filtro)
        {
            return this.Sessao.Query<T>().FirstOrDefault(filtro);
        }
    }
}
