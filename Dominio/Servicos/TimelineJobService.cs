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
    public class TimelineJobService
    {
        private readonly ITimelineJobRepositorio _timelineJobRepositorio;

        public TimelineJobService(ITimelineJobRepositorio timelineJobRepositorio)
        {
            _timelineJobRepositorio = timelineJobRepositorio;
        }

        public bool Cadastrar(TimelineJob timelineJob)
        {
            return _timelineJobRepositorio.Cadastrar(timelineJob);
        }

        public bool Remover(TimelineJob timelineJob)
        {
            return _timelineJobRepositorio.Remover(timelineJob);
        }

        public IList<TimelineJob> Listar(Expression<Func<TimelineJob, bool>> filtro)
        {
            return _timelineJobRepositorio.Listar(filtro);
        }

        public TimelineJob ObterPorId(int id)
        {
            return _timelineJobRepositorio.ObterPorId(id);
        }
    }
}
