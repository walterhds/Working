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
    public class CompromissoService
    {
        private readonly ICompromissoRepositorio _compromissoRepositorio;

        public CompromissoService(ICompromissoRepositorio compromissoRepositorio)
        {
            _compromissoRepositorio = compromissoRepositorio;
        }

        public bool Cadastrar(Compromisso compromisso)
        {
            return _compromissoRepositorio.Cadastrar(compromisso);
        }

        public IList<Compromisso> Listar(Expression<Func<Compromisso, bool>> filtro)
        {
            return _compromissoRepositorio.Listar(filtro);
        }

        public void Remover(Compromisso compromisso)
        {
            _compromissoRepositorio.Remover(compromisso);
        }

        public Compromisso ObterPorId(int id)
        {
            return _compromissoRepositorio.ObterPorId(id);
        }
    }
}
