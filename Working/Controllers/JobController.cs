using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class JobController : Controller
    {
        private readonly JobService _jobService;
        private readonly ClienteService _clienteService;
        private readonly FuncionarioService _funcionarioService;

        public JobController()
        {
            _jobService = Dependencia.Resolver<JobService>();
            _clienteService = Dependencia.Resolver<ClienteService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(int cliente, string briefing, string pecas, string descricao, int funcionario,
            DateTime dataCriacao, DateTime dataEstimativa, DateTime dataEntrega, string horasNecessarias,
            string situacao)
        {
            var job = new Job();
            job.DataRegistro = DateTime.Now;
            job.Funcionario = _funcionarioService.ObterPorId(funcionario);
            job.Cliente = _clienteService.ObterPorId(cliente);
            job.Briefing = briefing;
            job.Pecas = pecas;
            job.Descricao = descricao;
            job.DataCriacao = dataCriacao;
            job.DataEstimativa = dataEstimativa;
            job.DataEntrega = dataEntrega;
            job.HorasNecessarias = horasNecessarias;
            job.Situacao = situacao;
            _jobService.Cadastrar(job);
        }

        public JsonResult ListarJobsJson()
        {
            var job = _jobService.Listar(e => true).OrderBy(e => e.DataEstimativa);
            var lista = new List<Objeto>();
            foreach (var i in job)
            {
                lista.Add(new Objeto
                {
                    nome = i.Cliente.Nome,
                    DataCriacao = i.DataCriacao.Day.ToString("D2") + "/" + i.DataCriacao.ToString("MMMM"),
                    DataEstimativa = i.DataEstimativa.Day.ToString("D2") + "/" + i.DataEstimativa.ToString("MMMM"),
                    Situacao = i.Situacao
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
