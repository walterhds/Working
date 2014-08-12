using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Working.Models;

namespace Working.Controllers
{
    public class TimelineJobController : Controller
    {
        private readonly TimelineJobService _timelineJobService;
        private readonly FuncionarioService _funcionarioService;
        private readonly JobService _jobService;

        public TimelineJobController()
        {
            _timelineJobService = Dependencia.Resolver<TimelineJobService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _jobService = Dependencia.Resolver<JobService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(string comentario, int idJob)
        {
            var timelineJob = new TimelineJob();
            timelineJob.DataRegistro = DateTime.Now;
            timelineJob.Comentario = comentario;
            timelineJob.Job = _jobService.ObterPorId(idJob);
            timelineJob.Funcionario =
                _funcionarioService.ObterPorLogin((string)System.Web.HttpContext.Current.Session["usuario"]);
            timelineJob.Job = _jobService.ObterPorId(idJob);
            _timelineJobService.Cadastrar(timelineJob);
        }

        public JsonResult ListarTimelineJobJson(int id)
        {
            var job = _jobService.ObterPorId(id);
            var timelineJob = _timelineJobService.Listar(e => e.Job == job).OrderByDescending(e => e.DataRegistro);
            var lista = new List<Objeto>();
            var logado = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);

            foreach (var i in timelineJob)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    Data = i.DataRegistro.ToShortDateString(),
                    Hora = i.DataRegistro.ToString(),
                    Descricao = i.Comentario,
                    idFuncionario = i.Funcionario.Id,
                    Alterado = i.Funcionario.Nome,
                    IdLogado = logado.Id
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        } 
    }
}
