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
    public class JobService
    {
        private readonly IJobRepositorio _jobRepositorio;

        public JobService(IJobRepositorio jobRepositorio)
        {
            _jobRepositorio = jobRepositorio;
        }

        public bool Cadastrar(Job job)
        {
            return _jobRepositorio.Cadastrar(job);
        }

        public IList<Job> Listar(Expression<Func<Job, bool>>  filtro)
        {
            return _jobRepositorio.Listar(filtro);
        }

        public Job ObterPorId(int id)
        {
            return _jobRepositorio.ObterPorId(id);
        }

        public bool Remover(Job job)
        {
            return _jobRepositorio.Remover(job);
        }
    }
}
