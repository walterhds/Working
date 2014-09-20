using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Castle.MicroKernel.ModelBuilder.Descriptors;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using NHibernate.Criterion;
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
            var lista =
                _jobService.Listar(
                    e =>
                        e.Funcionario ==
                        _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]));
            return View(lista);
        }

        public JsonResult ListarMeusJobs()
        {
            var lista = new List<Objeto>();
            var listaJob =
                _jobService.Listar(
                    e =>
                        e.Funcionario ==
                        _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]) &&
                        e.Fase != "ENTREGUE");

            foreach (var i in listaJob)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    Cliente = i.Cliente.Nome,
                    nome = i.Nome,
                    Pecas = i.Peca,
                    DataCriacao = i.DataCriacao.Day.ToString("D2") + "/" + i.DataCriacao.ToString("MMMM"),
                    DataEstimativa = i.DataEstimativa.Day.ToString("D2") + "/" + i.DataEstimativa.ToString("MMMM"),
                    Situacao = i.Situacao
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public int CadastrarAjax(int cliente, string briefing)
        {
            var job = new Job();
            var dt = "01/01/01";
            job.DataRegistro = DateTime.Now;
            job.Cliente = _clienteService.ObterPorId(cliente);
            job.Briefing = briefing;
            job.Fase = "ANALISE";
            job.DataCriacao = Convert.ToDateTime(dt);
            job.DataEntrega = Convert.ToDateTime(dt);
            job.DataEstimativa = Convert.ToDateTime(dt);
            _jobService.Cadastrar(job);

            return _jobService.Listar(e => true).Last().Id;
        }

        public void AlterarAjax(int idJob, string fornecedores, string pecas, string descricao, string nome)
        {
            var dt = "01/01/01";
            var job = _jobService.ObterPorId(idJob);
            var pecasId = pecas.Split(',');
            var fornecedoresId = fornecedores.Split(',');
            job.DataRegistro = DateTime.Now;
            job.Descricao = descricao;
            job.DataCriacao = Convert.ToDateTime(dt);
            job.DataEstimativa = Convert.ToDateTime(dt);
            job.DataEntrega = Convert.ToDateTime(dt);
            job.Fase = "PRODUCAO";
            job.Nome = nome;
            IList<Fornecedor> listaFornecedor = new List<Fornecedor>();
            foreach (var i in fornecedoresId)
            {
                var fornecedor = _fornecedorService.ObterPorId(Convert.ToInt16(i));
                listaFornecedor.Add(fornecedor);
                job.Fornecedor = listaFornecedor;
            }

            IList<Peca> listaPeca = new List<Peca>();
            var listaPecas = _pecaService.Listar(e => true);
            foreach (var i in pecasId)
            {
                var peca = listaPecas.FirstOrDefault(e => e.Id == Convert.ToInt16(i));
                listaPeca.Add(peca);
                job.Peca = listaPeca;
            }
            _jobService.Cadastrar(job);
        }

        public void AlterarAjaxProducao(int idJob, string dtCriacao, string horas, string dtEstimativa, string funcionario, string situacao, string dtEntrega)
        {
            var job = _jobService.ObterPorId(idJob);
            if (horas.Contains(':'))
            {
                var hrs = horas.Split(':');
                if (hrs[1].Length > 1)
                {
                    job.HorasNecessarias = horas;
                }
                else
                {
                    horas = hrs[0] + ":" + hrs[1] + "0";
                    job.HorasNecessarias = horas;
                }
            }
            else
            {
                horas += ":00";
                job.HorasNecessarias = horas;
            }
            if (!string.IsNullOrWhiteSpace(dtEntrega))
            {
                job.Fase = "ENTREGUE";
                job.DataEntrega = Convert.ToDateTime(dtEntrega);
            }
            else
            {
                job.Fase = "EMPRODUCAO";
            }
            job.DataCriacao = Convert.ToDateTime(dtCriacao);
            job.DataEstimativa = Convert.ToDateTime(dtEstimativa);
            job.Funcionario = _funcionarioService.ObterPorId(Convert.ToInt16(funcionario));
            job.Situacao = _situacaoJobService.ObterPorId(Convert.ToInt16(situacao));
            job.DataRegistro = DateTime.Now;
            _jobService.Cadastrar(job);
        }

        public JsonResult ListarJobsJson()
        {
            var job =
                _jobService.Listar(e => true)
                    .OrderByDescending(e => e.DataEstimativa)
                    .ThenByDescending(e => e.DataCriacao)
                    .ThenByDescending(e => e.Id).Where(e => e.Fase != "ENTREGUE");
            var lista = new List<Objeto>();
            var fun = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
            foreach (var i in job)
            {
                if (i.Funcionario != null)
                {
                    lista.Add(new Objeto
                    {
                        id = i.Id,
                        Cliente = i.Cliente.Nome,
                        nome = i.Nome,
                        DataCriacao = i.DataCriacao.Day.ToString("D2") + "/" + i.DataCriacao.ToString("MMM"),
                        DataEstimativa = i.DataEstimativa.Day.ToString("D2") + "/" + i.DataEstimativa.ToString("MMM"),
                        Situacao = i.Situacao,
                        IdLogado = fun.Id,
                        idFuncionario = i.Funcionario.Id,
                        TemAcesso = fun.TemAcesso(i.Fase),
                        Descricao = fun.Cargo.Nome,
                        Fase = i.Fase
                    });
                }
                else
                {
                    lista.Add(new Objeto
                    {
                        id = i.Id,
                        Cliente = i.Cliente.Nome,
                        nome = i.Nome,
                        DataCriacao = i.DataCriacao.Day.ToString("D2") + "/" + i.DataCriacao.ToString("MMM"),
                        DataEstimativa = i.DataEstimativa.Day.ToString("D2") + "/" + i.DataEstimativa.ToString("MMM"),
                        Situacao = i.Situacao,
                        IdLogado = fun.Id,
                        idFuncionario = 0,
                        TemAcesso = fun.TemAcesso(i.Fase),
                        Descricao = fun.Cargo.Nome,
                        Fase = i.Fase
                    });
                }
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

        public JsonResult ListarJobsFuncionarioAlterar(int jid, int fid, string dataInicio, string dataFim)
        {
            var funcionario = _funcionarioService.ObterPorId(fid);
            var jobs = _jobService.Listar(e => e.Funcionario == funcionario);
            var lista = new List<Objeto>();

            foreach (var i in jobs)
            {
                if (i.DataEstimativa > Convert.ToDateTime(dataInicio))
                {
                    if (i.Id != jid)
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

        public JsonResult ObterJobPorIdJson(int id)
        {
            var job = _jobService.ObterPorId(id);
            var lista = new List<ObjetoJob>();
            if (job.Fase == "ANALISE")
            {
                lista.Add(new ObjetoJob
                {
                    Id = job.Id,
                    Briefing = job.Briefing,
                    Cliente = job.Cliente.Id.ToString(),
                    Fase = job.Fase
                });
            }
            else if(job.Fase == "PRODUCAO")
            {
                lista.Add(new ObjetoJob
                {
                    Id = job.Id,
                    Briefing = job.Briefing,
                    Descricao = job.Descricao,
                    Cliente = job.Cliente.Id.ToString(),
                    DataRegistro = job.DataRegistro.ToShortDateString(),
                    Fase = job.Fase,
                    Nome = job.Nome
                });
            }
            else
            {
                lista.Add(new ObjetoJob
                {
                    Id = job.Id,
                    Briefing = job.Briefing,
                    Descricao = job.Descricao,
                    SituacaoJob = job.Situacao.Id.ToString(),
                    Cliente = job.Cliente.Id.ToString(),
                    DataRegistro = job.DataRegistro.ToShortDateString(),
                    Fornecedor = job.Fornecedor,
                    Peca = job.Peca,
                    Fase = job.Fase,
                    DataCriacao = job.DataCriacao.ToShortDateString(),
                    DataEstimativa = job.DataEstimativa.ToShortDateString(),
                    HorasNecessarias = job.HorasNecessarias,
                    Funcionario = job.Funcionario.Id.ToString(),
                    Nome = job.Nome
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarJobFornecedoresJson(int id)
        {
            var job = _jobService.ObterPorId(id);
            var fornecedores = _fornecedorService.Listar(e => true);

            var lista = new List<Objeto>();
            foreach (var i in fornecedores)
            {
                if (job.Fornecedor.Contains(i))
                {
                    lista.Add(new Objeto
                    {
                        id = i.Id,
                        nome = i.Nome,
                        Alterado = i.Id.ToString()
                    });
                }
                else
                {
                    lista.Add(new Objeto
                    {
                        id = i.Id,
                        nome = i.Nome,
                        Alterado = "null"
                    });
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarJobPecasJson(int idJob, string id)
        {
            var fornecedoresId = id.Split(',');
            var job = _jobService.ObterPorId(idJob);
            var lista = new List<Objeto>();
            var cont = 0;
            var fornecedores = _fornecedorService.Listar(e => true);
            var pecas = _pecaService.Listar(e => true);
            foreach (var i in fornecedoresId)
            {
                //Estava buscando o fornecedor conforme o 'i' do foreach
                var fornecedor = fornecedores.FirstOrDefault(e => e.Id == Convert.ToInt16(i));
                //estava buscando as peças que continham o fornecedor acima
                var pecasFornecedor = pecas.Where(e => e.Fornecedor.Any(x => x.Id == Convert.ToInt16(i))).ToList();

                if (cont == 0)
                {
                    foreach (var j in pecasFornecedor)
                    {
                        if (job.Peca.Contains(j))
                        {
                            lista.Add(new Objeto
                            {
                                id = j.Id,
                                Descricao = j.Descricao,
                                Alterado = j.Id.ToString()
                            });
                        }
                        else
                        {
                            lista.Add(new Objeto
                            {
                                id = j.Id,
                                Descricao = j.Descricao,
                                Alterado = "null"
                            });
                        }
                    }
                    cont++;
                }
                else
                {
                    foreach (var k in pecasFornecedor)
                    {
                        if (lista.Count(objeto => objeto.id == k.Id) == 0)
                        {
                            if (job.Peca.Contains(k))
                            {
                                lista.Add(new Objeto
                                {
                                    id = k.Id,
                                    Descricao = k.Descricao,
                                    Alterado = k.Id.ToString()
                                });
                            }
                            else
                            {
                                lista.Add(new Objeto
                                {
                                    id = k.Id,
                                    Descricao = k.Descricao,
                                    Alterado = "null"
                                });
                            }
                        }
                    }
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarJobsSemContrato()
        {
            var listajobs = _jobService.Listar(e => e.Contrato == null);
            var lista = new List<Objeto>();

            foreach (var i in listajobs)
            {
                lista.Add(new Objeto
                {
                    nome = i.Nome,
                    id = i.Id
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
