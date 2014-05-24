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
        public BaseRepositorio(ISession sessao)
        {
            Sessao = sessao;
        }

        protected ISession Sessao { get; set; }

        //Cadastrar ou alterar
        public void Cadastrar(T entidade)
        {
            this.Sessao.BeginTransaction();
            try
            {
                this.Sessao.Merge(entidade);
                this.Sessao.Transaction.Commit();
            }
            catch (Exception e)
            {
                this.Sessao.Transaction.Rollback();
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
            this.Sessao.BeginTransaction();
            try
            {
                this.Sessao.Delete(entidade);
                this.Sessao.Transaction.Commit();
                this.Sessao.Transaction.Dispose();
                return true;
            }
            catch (Exception e)
            {
                this.Sessao.Transaction.Rollback();
                return false;
            }
        }
    }
}
