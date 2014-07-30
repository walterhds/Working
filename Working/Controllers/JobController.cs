using System;
using System.Collections;
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
        private readonly PecaService _pecaService;
        private readonly SituacaoJobService _situacaoJobService;
        private readonly FornecedorService _fornecedorService;

        public JobController()
        {
            _jobService = Dependencia.Resolver<JobService>();
            _clienteService = Dependencia.Resolver<ClienteService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _pecaService = Dependencia.Resolver<PecaService>();
            _situacaoJobService = Dependencia.Resolver<SituacaoJobService>();
            _fornecedorService = Dependencia.Resolver<FornecedorService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(int cliente, string briefing, string fornecedores, string pecas, string descricao, int funcionario,
            string dataCriacao, string dataEstimativa, string dataEntrega, string horasNecessarias,
            int situacao)
        {
            var job = new Job();
            var pecasId = pecas.Split(',');
            var fornecedoresId = fornecedores.Split(',');
            job.DataRegistro = DateTime.Now;
            job.Funcionario = _funcionarioService.ObterPorId(funcionario);
            job.Cliente = _clienteService.ObterPorId(cliente);
            job.Briefing = briefing;
            job.Descricao = descricao;
            job.DataCriacao = Convert.ToDateTime(dataCriacao);
            job.DataEstimativa = Convert.ToDateTime(dataEstimativa);
            job.DataEntrega = Convert.ToDateTime(dataEntrega);
            IList<Fornecedor> listaFornecedor = new List<Fornecedor>();
            foreach (var i in fornecedoresId)
            {
                var fornecedor = _fornecedorService.ObterPorId(Convert.ToInt16(i));
                listaFornecedor.Add(fornecedor);
                job.Fornecedor = listaFornecedor;
            }

            IList<Peca> listaPeca = new List<Peca>();
            foreach (var i in pecasId)
            {
                var peca = _pecaService.ObterPorId(Convert.ToInt16(i));
                listaPeca.Add(peca);
                job.Peca = listaPeca;
            }

            if (horasNecessarias.Contains(':'))
            {
                var horas = horasNecessarias.Split(':');
                if (horas[1].Length > 1)
                {
                    job.HorasNecessarias = horasNecessarias;
                }
                else
                {
                    horasNecessarias = horas[0] + ":" + horas[1] + "0";
                    job.HorasNecessarias = horasNecessarias;
                }
            }
            else
            {
                horasNecessarias += ":00";
                job.HorasNecessarias = horasNecessarias;
            }
            job.Situacao = _situacaoJobService.ObterPorId(situacao);
            _jobService.Cadastrar(job);
        }

        public JsonResult ListarJobsJson()
        {
            var job =
                _jobService.Listar(e => true)
                    .OrderByDescending(e => e.DataEstimativa)
                    .ThenByDescending(e => e.DataCriacao)
                    .ThenByDescending(e => e.Id);
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

        public JsonResult ListarJobsFuncionario(int fid, string dataInicio, string dataFim)
        {
            var funcionario = _funcionarioService.ObterPorId(fid);
            var jobs = _jobService.Listar(e => e.Funcionario == funcionario);
            var lista = new List<Objeto>();

            foreach (var i in jobs)
            {
                if (i.DataEstimativa > Convert.ToDateTime(dataInicio))
                {
                    lista.Add(new Objeto
                    {
                        HorasNecessarias = i.HorasNecessarias,
                        DataCriacao =
                            i.DataCriacao.ToString("MM") + "/" + i.DataCriacao.Day.ToString("D2") + "/" +
                            i.DataCriacao.Year,
                        DataEstimativa =
                            i.DataEstimativa.ToString("MM") + "/" + i.DataEstimativa.Day.ToString("D2") + "/" +
                            i.DataEstimativa.Year,
                        QuantidadeHora = funcionario.QuantidadeHora
                    });
                }
            }

            if (lista.Count == 0)
            {
                lista.Add(new Objeto
                {
                    HorasNecessarias = "null",
                    QuantidadeHora = funcionario.QuantidadeHora
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
