//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web.Mvc;
//using Dependencias;
//using Dominios.Entidades;
//using Dominios.Servicos;
//using MVCCerto.NewModels;
//using MvcPaging;
//using PortalConducao.Web.Models;

//namespace PortalConducao.Web.Controllers
//{
//    [Autorizar]
//    public class ConducaoController : Controller
//    {

//        private readonly AuditorConducaoObservacaoService _auditorConducaoObservacao;
//        private readonly ConducaoCabecalhoService _conducaoCabecalhoService;
//        private readonly ConducaoCorpoService _conducaoCorpoService;
//        private readonly ConducaoCompletaService _conducaoCompletaService;
//        private readonly CombinacoesService _combinacaoService;
//        private readonly UPService _upService;
//        private readonly TrechoService _trechoService;
//        private readonly SbService _sbService;
//        private readonly AjustesService _ajustesService;
//        private readonly MaquinaService _MaquinaService;
//        private readonly Pessoal Usuario = (Pessoal)System.Web.HttpContext.Current.Session["usuario"];
//        private readonly PessoalService _usuarioService;
//        private const int DefaultPageSize = 100;

//        public static IList<Conducao_Corpo> ConducaoCorpoFake = new List<Conducao_Corpo>();
//        public ConducaoController()
//        {
//            _auditorConducaoObservacao = Dependecias.Resolver<AuditorConducaoObservacaoService>();
//            _conducaoCabecalhoService = Dependecias.Resolver<ConducaoCabecalhoService>();
//            _conducaoCorpoService = Dependecias.Resolver<ConducaoCorpoService>();
//            _conducaoCompletaService = Dependecias.Resolver<ConducaoCompletaService>();
//            _combinacaoService = Dependecias.Resolver<CombinacoesService>();
//            _upService = Dependecias.Resolver<UPService>();
//            _trechoService = Dependecias.Resolver<TrechoService>();
//            _sbService = Dependecias.Resolver<SbService>();
//            _usuarioService = Dependecias.Resolver<PessoalService>();
//            _ajustesService = Dependecias.Resolver<AjustesService>();
//            _MaquinaService = Dependecias.Resolver<MaquinaService>();

//        }

//        public ActionResult Listar(string codigoConducao, string up, string tipo, string trecho, string statusPadrao, string StatusSentido, int? page)
//        {
//            var home = new HomeController();
//            home.CalcularPendencias();
//            var bolsa = new ConjuntosListasEObj2();
//            ViewData["codigoConducao"] = codigoConducao;
//            ViewData["StatusSentido"] = StatusSentido;
//            ViewData["up"] = up;
//            ViewData["tipo"] = tipo;
//            ViewData["StatusPadrao"] = statusPadrao;
//            ViewData["Trecho"] = trecho;
//            int paginaatual = page.HasValue ? page.Value : 1;
//            var regra = _ajustesService.ObterporId2(e => e.Status == "Atual");


//            IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Cabecalho");
//            IEqualityComparer<ConducaoCompleta> b = new lambdaComparer<ConducaoCompleta>("IdaVolta");
//            if (Usuario.Cargo.Descricao.ToLower() != "auditor")
//            {

//                if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(trecho)) && (string.IsNullOrWhiteSpace(statusPadrao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(e => e.CStatus.ToString().ToLower() == "alan").Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(trecho)) && (string.IsNullOrWhiteSpace(statusPadrao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(trecho)) && (string.IsNullOrWhiteSpace(statusPadrao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(trecho)) && (string.IsNullOrWhiteSpace(statusPadrao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(statusPadrao) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(statusPadrao) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && string.IsNullOrWhiteSpace(statusPadrao) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && string.IsNullOrWhiteSpace(statusPadrao) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.Cabecalho.Trecho.Descricao == trecho && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(up) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(up))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(codigoConducao) && string.IsNullOrWhiteSpace(up))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(up))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Trecho.Descricao == trecho && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.CStatus == char.Parse(statusPadrao)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(statusPadrao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.Cabecalho.Trecho.Descricao == trecho && p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d").Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(trecho)) && (string.IsNullOrWhiteSpace(codigoConducao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho) && (string.IsNullOrWhiteSpace(codigoConducao)) && (string.IsNullOrWhiteSpace(up)))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(up))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(statusPadrao))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Trecho.Descricao == trecho && p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d").Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho) && (string.IsNullOrWhiteSpace(codigoConducao)) && (string.IsNullOrWhiteSpace(statusPadrao)))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower())).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho) && (string.IsNullOrWhiteSpace(codigoConducao)) && (string.IsNullOrWhiteSpace(tipo)))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(tipo)))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Trecho.Descricao == trecho && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(codigoConducao)) &&
//                         (string.IsNullOrWhiteSpace(tipo)))
//                {
//                    bolsa.PagedConducaoCompleta =
//                        _conducaoCompletaService.Listar(
//                            p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower())
//                            .ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && p.CStatus.ToString().ToLower() == char.Parse(statusPadrao).ToString().ToLower()).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                if (StatusSentido == "Ida")
//                {
//                    bolsa.PagedConducaoCompleta = bolsa.PagedConducaoCompleta.Where(e => e.IdaVolta == "Ida").ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (StatusSentido == "Volta")
//                {
//                    bolsa.PagedConducaoCompleta = bolsa.PagedConducaoCompleta.Where(e => e.IdaVolta == "Volta").ToPagedList(paginaatual, DefaultPageSize);
//                }
//            }
//            else
//            {
//                if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(trecho))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(e => e.CStatus.ToString().ToLower() == "alan").Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(trecho))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(trecho))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && (string.IsNullOrWhiteSpace(tipo) && (string.IsNullOrWhiteSpace(trecho))))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo) && string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i" && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(codigoConducao))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && ((p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i") && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up) && string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && ((p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i") && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(up))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && ((p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i") && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(trecho))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.Id == int.Parse(up) && p.Cabecalho.Combinacao.STipo == tipo && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && ((p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i") && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (string.IsNullOrWhiteSpace(tipo))
//                {
//                    bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.sNome_Up == up && p.Cabecalho.Trecho.Descricao == trecho && p.Cabecalho.SCodigoConducao.ToLower().Contains(codigoConducao.ToLower()) && ((p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d") || p.CStatus.ToString().ToLower() == "i") && p.DDataAlteracao != null && p.DDataAlteracao >= DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);
//                }
//                if (StatusSentido == "Ida")
//                {
//                    bolsa.PagedConducaoCompleta = bolsa.PagedConducaoCompleta.Where(e => e.IdaVolta == "Ida").ToPagedList(paginaatual, DefaultPageSize);
//                }
//                else if (StatusSentido == "Volta")
//                {
//                    bolsa.PagedConducaoCompleta = bolsa.PagedConducaoCompleta.Where(e => e.IdaVolta == "Volta").ToPagedList(paginaatual, DefaultPageSize);
//                }
//                //else if (string.IsNullOrWhiteSpace(codigoConducao) && (string.IsNullOrWhiteSpace(tipo)&& (string.IsNullOrWhiteSpace(statusPadrao))))
//                //{

//                //bolsa.PagedConducaoCompleta = _conducaoCompletaService.Listar(p => p.Cabecalho.Trecho.Up.sNome_Up == up && (p.CStatus.ToString().ToLower() == "a" || p.CStatus.ToString().ToLower() == "d")||p.CStatus.ToString().ToLower() == "i"&&p.DDataAlteracao!=null&&p.DDataAlteracao>=DateTime.Now.AddDays(-regra.PadraoVisualisarAuditor)).Distinct(a).ToPagedList(paginaatual, DefaultPageSize);

//                //}

//            }

//            bolsa.Up = _upService.Listar(e => true);
//            return View(bolsa);
//        }
//        public void Ativar(int id, FormCollection form)
//        {


//            var cabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            var conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao && e.CStatus.ToString().ToLower() == "c").ToList();
//            foreach (var t in conducaoCompletaAlterada)
//            {
//                t.DDataAlteracao = DateTime.Now;
//                t.CStatus = 'A';
//                t.Cabecalho.DVigencia = DateTime.Now;
//                _conducaoCabecalhoService.Cadastrar(t.Cabecalho);
//                _conducaoCompletaService.Cadastrar(t);
//            }

//            var versoaantiga = cabecalho.Versao - 1;
//            var conducaoCompletaAntiga =
//                _conducaoCompletaService.Listar(
//                    e =>
//                        e.Cabecalho.Versao == versoaantiga &&
//                        e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao);
//            foreach (var t in conducaoCompletaAntiga)
//            {
//                t.CStatus = 'I';
//                t.DDataAlteracao = DateTime.Now;
//                _conducaoCompletaService.Cadastrar(t);
//            }
//        }
//        public void Apagar(int id, string id2, FormCollection form)
//        {
//            IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Corpo");
//            IEqualityComparer<ConducaoCompleta> b = new lambdaComparer<ConducaoCompleta>("Cabecalho");

//            var cabecalho = _conducaoCabecalhoService.ObterUporId(id);

//            var conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao && e.CStatus.ToString().ToLower() == id2.ToLower()).ToList();
//            foreach (var t in conducaoCompletaAlterada)
//            {
//                _conducaoCompletaService.Remover(t);

//            }
//            foreach (var t2 in conducaoCompletaAlterada.Distinct(a))
//            {
//                _conducaoCorpoService.Remover(t2.Corpo);
//            }
//            var cabecalhsos = conducaoCompletaAlterada.Distinct(b).ToList();
//            foreach (var t2 in cabecalhsos)
//            {
//                _conducaoCabecalhoService.Remover(t2.Cabecalho);
//            }

//            var versoaantiga = cabecalho.Versao - 1;
//            var conducaoCompletaAntiga =
//                _conducaoCompletaService.Listar(
//                    e =>
//                        e.Cabecalho.Versao == versoaantiga &&
//                        e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao);
//            foreach (var t in conducaoCompletaAntiga)
//            {
//                if (t.CStatus.ToString().ToLower() == 'd'.ToString().ToLower())
//                {
//                    t.DDataAlteracao = DateTime.Now;
//                    t.CStatus = 'A';
//                    _conducaoCompletaService.Cadastrar(t);
//                }
//                else if (t.CStatus.ToString().ToLower() == 'i'.ToString().ToLower())
//                {
//                    t.DDataAlteracao = DateTime.Now;
//                    t.CStatus = 'A';
//                    _conducaoCompletaService.Cadastrar(t);
//                }
//            }
//        }

//        public void Cancelar(int id, FormCollection form)
//        {

//            var cabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            var conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao && e.CStatus == 'E').ToList();
//            foreach (var t in conducaoCompletaAlterada)
//            {
//                _conducaoCompletaService.Remover(t);

//            }
//            foreach (var t2 in conducaoCompletaAlterada)
//            {
//                _conducaoCorpoService.Remover(t2.Corpo);
//            }
//            _conducaoCabecalhoService.Remover(cabecalho);

//        }
//        public ActionResult ListarPadrao(int id, int? page)
//        {
//            IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Cabecalho");
//            IEqualityComparer<ConducaoCabecalho> b = new lambdaComparer<ConducaoCabecalho>("Combinacao");

//            var bolsa = new ConjuntosListasEObj2();
//            var conducaoCorpo = new Dominios.Entidades.Conducao_Corpo();
//            var conducaoCabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            bolsa.Conducao_Completa = _conducaoCompletaService.ObterporId2(e => e.Cabecalho.Id == id);
//            var statusatualconducao = bolsa.Conducao_Completa.CStatus;
//            var conducaoCompleta = _conducaoCompletaService.Listar(e => e.Cabecalho.SCodigoConducao == conducaoCabecalho.SCodigoConducao && e.CStatus.ToString().ToLower() == statusatualconducao.ToString().ToLower());
//            bolsa.Conducao_Cabecalho = conducaoCabecalho;
//            if (bolsa.Conducao_Completa.IdaVolta == "Ida")
//            {
//                bolsa.Conducao_Completas =
//                    _conducaoCompletaService.Listar(
//                        e =>
//                            e.Cabecalho.Id == id &&
//                            e.CStatus.ToString().ToLower() == statusatualconducao.ToString().ToLower())
//                        .OrderBy(e => e.Corpo.KmInicio);
//            }
//            else
//            {
//                bolsa.Conducao_Completas = _conducaoCompletaService.Listar(e => e.Cabecalho.Id == id && e.CStatus.ToString().ToLower() == statusatualconducao.ToString().ToLower()).OrderByDescending(e => e.Corpo.KmInicio);
//            }

//            bolsa.AuditorConducaoObservacaos =
//                _auditorConducaoObservacao.Listar(
//                    e =>
//                        e.Cabecalho.SCodigoConducao == conducaoCabecalho.SCodigoConducao);
//            bolsa.AuditorConducaoObservacao = _auditorConducaoObservacao.AuditorConducaoObterporId2(
//                    e =>
//                        e.Cabecalho.SCodigoConducao == conducaoCabecalho.SCodigoConducao);
//            bolsa.EnumerableConducao_Cabecalhos = _conducaoCabecalhoService.Listar(e => e.SCodigoConducao == bolsa.Conducao_Cabecalho.SCodigoConducao && e.Versao == bolsa.Conducao_Cabecalho.Versao).Distinct(b);

//            return View(bolsa);
//        }
//        public ActionResult Pesquisar(string cond)
//        {
//            var lista = _conducaoCabecalhoService.Listar(e => e.SCodigoConducao == cond && e.Id > 10 && e.DVigencia < DateTime.Now);
//            return View();
//        }


//        public ActionResult Novo()
//        {

//            var listas = new ConjuntosListadeDados1();
//            var listaCabecalhos = _conducaoCabecalhoService.Listar(e => true);
//            var listaUps = _upService.Listar(e => true);
//            var listaCombinacoes = _combinacaoService.Listar(e => true);
//            var listaTrechos = _trechoService.Listar(e => true);
//            var listaConducaoCorpos = _conducaoCorpoService.Listar(e => true);
//            var listaMaquinas = _MaquinaService.Listar(e => true);
//            listas.Combinacoes = listaCombinacoes;
//            listas.Trechos = listaTrechos;
//            listas.Conducao_Cabecalho = listaCabecalhos;
//            listas.Conducao_Corpo = listaConducaoCorpos;
//            listas.Ups = listaUps;
//            listas.Maquinas = listaMaquinas;
//            ViewData["UPS"] = new SelectList(_upService.Listar(e => true), "ID", "DESCRICAO");

//            return View(listas);

//        }





//        public IList<Sb> Sbs = new List<Sb>();
//        public IList<Combinacao> Comb = new List<Combinacao>();


//        [HttpPost]
//        public ActionResult Novo(FormCollection form)
//        {
//            IList<Conducao_Corpo> conducaoCorpos = new List<Conducao_Corpo>();
//            IList<ConducaoCabecalho> conducaoCabecalhos = new List<ConducaoCabecalho>();
//            IList<Combinacao> combinacoes = new List<Combinacao>();
//            var usuario = new Usuario();
//            var quantidadedeTipoLotacao = 0;
//            string tipoLotação1 = null, pesoInicial1 = null, pesoFinal1 = null;
//            string tipoLotação2 = null, pesoInicial2 = null, pesoFinal2 = null;
//            string tipoLotação3 = null, pesoInicial3 = null, pesoFinal3 = null;
//            string tipoLotação4 = null, pesoInicial4 = null, pesoFinal4 = null;
//            string tipoLotação5 = null, pesoInicial5 = null, pesoFinal5 = null;
//            string tipoLotação6 = null, pesoInicial6 = null, pesoFinal6 = null;
//            var combinacao1 = new Combinacao();
//            var combinacao2 = new Combinacao();
//            var combinacao3 = new Combinacao();
//            var combinacao4 = new Combinacao();
//            var combinacao5 = new Combinacao();
//            var combinacao6 = new Combinacao();
//            //string[] sbs = form["SB"].Split(',');
//            if (form["cbx_Tipodelotacao1"] != null)
//            {
//                tipoLotação1 = form["cbx_Tipodelotacao1"].ToString();
//                pesoInicial1 = form["iPeso_Incial1"].Replace('.', ',');
//                pesoFinal1 = form["iPeso_Final1"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao1 = _combinacaoService.ObterUporId(int.Parse(tipoLotação1));
//                combinacoes.Add(combinacao1);

//            }
//            if (form["cbx_Tipodelotacao2"] != null)
//            {
//                tipoLotação2 = form["cbx_Tipodelotacao2"].ToString();
//                pesoInicial2 = form["iPeso_Incial2"].Replace('.', ',');
//                pesoFinal2 = form["iPeso_Final2"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao2 = _combinacaoService.ObterUporId(int.Parse(tipoLotação2));
//                combinacoes.Add(combinacao2);


//            }
//            if (form["cbx_Tipodelotacao3"] != null)
//            {
//                tipoLotação3 = form["cbx_Tipodelotacao3"].ToString();
//                pesoInicial3 = form["iPeso_Incial3"].Replace('.', ',');
//                pesoFinal3 = form["iPeso_Final3"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao3 = _combinacaoService.ObterUporId(int.Parse(tipoLotação3));
//                combinacoes.Add(combinacao3);

//            }
//            if (form["cbx_Tipodelotacao4"] != null)
//            {
//                tipoLotação4 = form["cbx_Tipodelotacao4"].ToString();
//                pesoInicial4 = form["iPeso_Incial4"].Replace('.', ',');
//                pesoFinal4 = form["iPeso_Final4"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao4 = _combinacaoService.ObterUporId(int.Parse(tipoLotação4));
//                combinacoes.Add(combinacao4);

//            }
//            if (form["cbx_Tipodelotacao5"] != null)
//            {
//                tipoLotação5 = form["cbx_Tipodelotacao5"].ToString();
//                pesoInicial5 = form["iPeso_Incial5"].Replace('.', ',');
//                pesoFinal5 = form["iPeso_Final5"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao5 = _combinacaoService.ObterUporId(int.Parse(tipoLotação5));
//                combinacoes.Add(combinacao5);

//            }
//            if (form["cbx_Tipodelotacao6"] != null)
//            {
//                tipoLotação6 = form["cbx_Tipodelotacao6"].ToString();
//                pesoInicial6 = form["iPeso_Incial6"].Replace('.', ',');
//                pesoFinal6 = form["iPeso_Final6"].Replace('.', ',');
//                quantidadedeTipoLotacao++;
//                combinacao6 = _combinacaoService.ObterUporId(int.Parse(tipoLotação6));
//                combinacoes.Add(combinacao6);

//            }

//            //var listadeSbs = _sbService.Listar(e => true);
//            var contador = 0;
//            var contarQuantidadeCorpo = 0;
//            var contenAlgo = form["km" + contador].ToString();
//            while (contenAlgo != null)
//            {
//                contarQuantidadeCorpo++;
//                var conducaoCorpo = new Conducao_Corpo();
//                var kminicio = form["km" + contador.ToString()].Replace('.', ',');
//                var loco1 = form["sStatus_Loc1" + contador.ToString()];
//                var loco2 = form["sStatus_Loc2" + contador.ToString()];
//                var loco3 = form["sStatus_Loc3" + contador.ToString()];
//                var loco4 = form["sStatus_Loc4" + contador.ToString()];
//                var loco5 = form["sStatus_Loc5" + contador.ToString()];
//                var loco6 = form["sStatus_Loc6" + contador.ToString()];
//                var descricao = form["descricao" + contador.ToString()];
//                var kmFim = form["kmfim" + contador.ToString()].Replace('.', ',');
//                var observacões = form["obs" + contador.ToString()];
//                TryUpdateModel(conducaoCorpo, form);
//                conducaoCorpo.Descricao_Conducao = descricao;
//                conducaoCorpo.KmInicio = float.Parse(kminicio);
//                conducaoCorpo.KmFim = float.Parse(kmFim);
//                conducaoCorpo.sObservacoes = observacões;
//                conducaoCorpo.sStatus_Loc1 = loco1;
//                conducaoCorpo.sStatus_Loc2 = loco2;
//                conducaoCorpo.sStatus_Loc3 = loco3;
//                conducaoCorpo.sStatus_Loc4 = loco4;
//                conducaoCorpo.sStatus_Loc5 = loco5;
//                conducaoCorpo.sStatus_Loc6 = loco6;
//                if (form["cbx_Opcoestrecho"].ToString() == "Ida")
//                {
//                    conducaoCorpo.Sb =
//                        _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kminicio) && e.KmFim >= float.Parse(kmFim));

//                }
//                else
//                {
//                    conducaoCorpo.Sb =
//                    _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kmFim) && e.KmFim >= float.Parse(kminicio));

//                }

//                conducaoCorpos.Add(conducaoCorpo);
//                _conducaoCorpoService.Cadastrar(conducaoCorpos[contador]);

//                var verificador = contarQuantidadeCorpo;

//                contenAlgo = form["km" + verificador] != null ? form["km" + verificador].ToString(CultureInfo.InvariantCulture) : null;

//                for (int i = 0; i < 6; i++)
//                {
//                    if (contenAlgo == null)
//                    {
//                        contenAlgo = form["km" + verificador] != null ? form["km" + verificador].ToString() : null;
//                        if (contenAlgo == null)
//                        {
//                            contarQuantidadeCorpo++;
//                        }
//                    }
//                    verificador++;
//                }
//                contador++;



//            }
//            var trecho = _trechoService.ObterUporId((Convert.ToInt32(form["TRECHO"])));
//            var quantcabecalho = _conducaoCabecalhoService.Listar(e => e.Trecho.Up.sNome_Up == trecho.Up.sNome_Up).Count + 1;
//            string codigogerado = trecho.Up.sNome_Up + quantcabecalho.ToString();
//            int numeroparaverificar2 = -1;
//            for (var i = 0; i < quantidadedeTipoLotacao; i++)
//            {
//                var conducaoCabecalho = new ConducaoCabecalho();
//                TryUpdateModel(conducaoCabecalho, form);
//                conducaoCabecalho.Trecho = trecho;
//                if (combinacoes[i] == combinacao1)
//                {
//                    numeroparaverificar2 = 0;
//                }
//                else if (combinacoes[i] == combinacao2)
//                {
//                    numeroparaverificar2 = 1;
//                }
//                else if (combinacoes[i] == combinacao3)
//                {
//                    numeroparaverificar2 = 2;
//                }
//                else if (combinacoes[i] == combinacao4)
//                {
//                    numeroparaverificar2 = 3;
//                }
//                else if (combinacoes[i] == combinacao5)
//                {
//                    numeroparaverificar2 = 4;
//                }
//                else if (combinacoes[i] == combinacao6)
//                {
//                    numeroparaverificar2 = 5;
//                }
//                if (numeroparaverificar2 == 0)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação1));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial1.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal1.Replace('.', ','));
//                }
//                if (numeroparaverificar2 == 1)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação2));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial2.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal2.Replace('.', ','));
//                }
//                if (numeroparaverificar2 == 2)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação3));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial3.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal3.Replace('.', ','));
//                }
//                if (numeroparaverificar2 == 3)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação4));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial4.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal4.Replace('.', ','));
//                }
//                if (numeroparaverificar2 == 4)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação5));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial5.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal5.Replace('.', ','));
//                }
//                if (numeroparaverificar2 == 5)
//                {
//                    conducaoCabecalho.Combinacao = _combinacaoService.ObterUporId(int.Parse(tipoLotação6));
//                    conducaoCabecalho.FPesoIncial = int.Parse(pesoInicial6.Replace('.', ','));
//                    conducaoCabecalho.FPesoFinal = int.Parse(pesoFinal6.Replace('.', ','));
//                }

//                conducaoCabecalho.SCodigoConducao = codigogerado;

//                _conducaoCabecalhoService.Cadastrar(conducaoCabecalho);
//                conducaoCabecalhos.Add(conducaoCabecalho);


//                foreach (Conducao_Corpo t in conducaoCorpos)
//                {
//                    var conducaoCompleta = new ConducaoCompleta();

//                    TryUpdateModel(conducaoCompleta, form);
//                    conducaoCompleta.Cabecalho = conducaoCabecalho;
//                    conducaoCompleta.DDataRegistro = DateTime.Now;


//                    conducaoCompleta.DDataAlteracao = DateTime.Now;
//                    conducaoCompleta.DDataRegistroEspecialista = Usuario.Cargo.Descricao.ToString().ToLower() == "especialista" ? DateTime.Now : (DateTime?)null;
//                    conducaoCompleta.CStatus = Usuario.Cargo.Descricao.ToString().ToLower() == "especialista" ? 'C' : 'E';
//                    conducaoCompleta.Usuario = Usuario;
//                    conducaoCompleta.Corpo = t;
//                    conducaoCompleta.IdaVolta = form["cbx_Opcoestrecho"].ToString();
//                    _conducaoCompletaService.Cadastrar(conducaoCompleta);
//                }


//            }

//            return RedirectToAction("Listar");
//        }


//        public ActionResult Deletar(int id)
//        {
//            //var Cond = _Conducao_Service.ObterUporID((id));

//            return View();
//        }


//        [HttpPost]
//        public ActionResult Deletar(int id, FormCollection form)
//        {
//            //var up = _Conducao_Service.ObterUporID((id));
//            //_Conducao_Service.Remover(up);
//            return RedirectToAction("Listar");
//        }
//        public ActionResult Alterar1()
//        {

//            return View();
//        }
//        public ActionResult Alterar(int id)
//        {
//            var bolsa = new ConjuntosListasEObj2();
//            var conducaoCorpo = new Dominios.Entidades.Conducao_Corpo();
//            //int paginaatual = page.HasValue ? page.Value : 1;
//            IEqualityComparer<ConducaoCabecalho> b = new lambdaComparer<ConducaoCabecalho>("Combinacao");
//            IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Corpo");



//            var conducaoCabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            bolsa.Up = _upService.Listar(e => true);
//            bolsa.Trechos = _trechoService.Listar(e => e.Up.sNome_Up == conducaoCabecalho.Trecho.Up.sNome_Up);
//            bolsa.Conducao_Cabecalho = conducaoCabecalho;
//            bolsa.Conducao_Completa = _conducaoCompletaService.ObterporId2(e => e.Cabecalho.Id == id);
//            if (bolsa.Conducao_Completa.IdaVolta == "Ida")
//            {
//                bolsa.Conducao_Completas =
//                    _conducaoCompletaService.Listar(e => e.Cabecalho.Id == id).OrderBy(e => e.Corpo.KmInicio);
//            }
//            else
//            {
//                bolsa.Conducao_Completas =
//                   _conducaoCompletaService.Listar(e => e.Cabecalho.Id == id).OrderByDescending(e => e.Corpo.KmInicio);
//            }

//            bolsa.EnumerableConducao_Cabecalhos = _conducaoCabecalhoService.Listar(e => e.SCodigoConducao == bolsa.Conducao_Cabecalho.SCodigoConducao && e.Versao == bolsa.Conducao_Cabecalho.Versao).Distinct(b);

//            bolsa.Usuario = Usuario;
//            bolsa.Maquinas = _MaquinaService.Listar(e => true);
//            //PopularCorpoFake(bolsa, null);
//            return View(bolsa);
//        }


//        [HttpPost]
//        public ActionResult Alterar(int id, FormCollection form)
//        {
//            IList<Conducao_Corpo> conducaoCorpos = new List<Conducao_Corpo>();
//            IList<ConducaoCabecalho> conducaoCabecalhos = new List<ConducaoCabecalho>();
//            var quantidadedeTipoLotacao = 0;

//            string tipoLotação1 = "-1", pesoInicial1 = null, pesoFinal1 = null;
//            string tipoLotação2 = "-1", pesoInicial2 = null, pesoFinal2 = null;
//            string tipoLotação3 = "-1", pesoInicial3 = null, pesoFinal3 = null;
//            string tipoLotação4 = "-1", pesoInicial4 = null, pesoFinal4 = null;
//            string tipoLotação5 = "-1", pesoInicial5 = null, pesoFinal5 = null;
//            string tipoLotação6 = "-1", pesoInicial6 = null, pesoFinal6 = null;
//            var cabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            var conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho == cabecalho);
//            if (conducaoCompletaAlterada[0].CStatus.ToString().ToLower() == "e")
//            {
//                if (form["cbx_Tipodelotacao1"] != null)
//                {
//                    tipoLotação1 = form["cbx_Tipodelotacao1"].ToString();
//                    pesoInicial1 = form["iPeso_Incial1"].Replace('.', ',');
//                    pesoFinal1 = form["iPeso_Final1"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                if (form["cbx_Tipodelotacao2"] != null)
//                {
//                    tipoLotação2 = form["cbx_Tipodelotacao2"].ToString();
//                    pesoInicial2 = form["iPeso_Incial2"].Replace('.', ',');
//                    pesoFinal2 = form["iPeso_Final2"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                if (form["cbx_Tipodelotacao3"] != null)
//                {
//                    tipoLotação3 = form["cbx_Tipodelotacao3"].ToString();
//                    pesoInicial3 = form["iPeso_Incial3"].Replace('.', ',');
//                    pesoFinal3 = form["iPeso_Final3"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                if (form["cbx_Tipodelotacao4"] != null)
//                {
//                    tipoLotação4 = form["cbx_Tipodelotacao4"].ToString();
//                    pesoInicial4 = form["iPeso_Incial4"].Replace('.', ',');
//                    pesoFinal4 = form["iPeso_Final4"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                if (form["cbx_Tipodelotacao5"] != null)
//                {
//                    tipoLotação5 = form["cbx_Tipodelotacao5"].ToString();
//                    pesoInicial5 = form["iPeso_Incial5"].Replace('.', ',');
//                    pesoFinal5 = form["iPeso_Final5"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                if (form["cbx_Tipodelotacao6"] != null)
//                {
//                    tipoLotação6 = form["cbx_Tipodelotacao6"].ToString();
//                    pesoInicial6 = form["iPeso_Incial6"].Replace('.', ',');
//                    pesoFinal6 = form["iPeso_Final6"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                IEqualityComparer<ConducaoCompleta> f = new lambdaComparer<ConducaoCompleta>("Corpo");
//                var alterada = (List<ConducaoCompleta>)conducaoCompletaAlterada;
//                var quantidadecabecalhos1 =
//                      _conducaoCabecalhoService.Listar(
//                          e =>
//                              e.SCodigoConducao == alterada[0].Cabecalho.SCodigoConducao &&
//                              e.Versao == alterada[0].Cabecalho.Versao);
//                var contador = 0;
//                var contarQuantidadeCorpo = 0;
//                var contenAlgo = form["km1"];
//                conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho.SCodigoConducao == quantidadecabecalhos1[0].SCodigoConducao && e.CStatus.ToString().ToLower() == "e").Distinct(f).ToList();
//                while (contenAlgo != null)
//                {
//                    contarQuantidadeCorpo++;
//                    //foreach (var t1 in quantidadecabecalhos1)
//                    //{

//                    var kminicio = form["km" + contarQuantidadeCorpo.ToString()].Replace('.', ',');
//                    var loco1 = form["sStatus_Loc1" + contarQuantidadeCorpo].ToString();
//                    var loco2 = form["sStatus_Loc2" + contarQuantidadeCorpo].ToString();
//                    var loco3 = form["sStatus_Loc3" + contarQuantidadeCorpo].ToString();
//                    var loco4 = form["sStatus_Loc4" + contarQuantidadeCorpo].ToString();
//                    var loco5 = form["sStatus_Loc5" + contarQuantidadeCorpo].ToString();
//                    var loco6 = form["sStatus_Loc6" + contarQuantidadeCorpo].ToString();
//                    var descricao = form["descricao" + contarQuantidadeCorpo].ToString();
//                    var kmFim = form["kmfim" + contarQuantidadeCorpo].ToString().Replace('.', ',');
//                    var observacões = form["obs" + contarQuantidadeCorpo].ToString();
//                    var rodape = form["SRodape"].ToString();


//                    if (contador < conducaoCompletaAlterada.Count)
//                    {
//                        TryUpdateModel(conducaoCompletaAlterada[contador].Corpo, form);
//                        conducaoCompletaAlterada[contador].Corpo.Descricao_Conducao = descricao;
//                        conducaoCompletaAlterada[contador].Corpo.KmInicio = float.Parse(kminicio);
//                        conducaoCompletaAlterada[contador].Corpo.KmFim = float.Parse(kmFim);
//                        conducaoCompletaAlterada[contador].Corpo.sObservacoes = observacões;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc1 = loco1;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc2 = loco2;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc3 = loco3;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc4 = loco4;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc5 = loco5;
//                        conducaoCompletaAlterada[contador].Corpo.sStatus_Loc6 = loco6;
//                        if (conducaoCompletaAlterada[contador].IdaVolta == "Ida")
//                        {

//                            conducaoCompletaAlterada[contador].Corpo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kminicio) && e.KmFim >= float.Parse(kmFim));
//                        }

//                        else
//                        {

//                            conducaoCompletaAlterada[contador].Corpo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kmFim) && e.KmFim >= float.Parse(kminicio));

//                        }


//                        conducaoCorpos.Add(conducaoCompletaAlterada[contador].Corpo);
//                        _conducaoCorpoService.Cadastrar(conducaoCompletaAlterada[contador].Corpo);
//                        conducaoCompletaAlterada[contador].SRodape = rodape;
//                        conducaoCompletaAlterada[contador].DDataAlteracao = DateTime.Now;
//                        _conducaoCompletaService.Cadastrar(conducaoCompletaAlterada[contador]);
//                    }
//                    else
//                    {
//                        var conducaoCorpoNovo = new Conducao_Corpo();
//                        TryUpdateModel(conducaoCorpoNovo, form);
//                        conducaoCorpoNovo.Descricao_Conducao = descricao;
//                        conducaoCorpoNovo.KmInicio = float.Parse(kminicio);
//                        conducaoCorpoNovo.KmFim = float.Parse(kmFim);
//                        conducaoCorpoNovo.sObservacoes = observacões;
//                        conducaoCorpoNovo.sStatus_Loc1 = loco1;
//                        conducaoCorpoNovo.sStatus_Loc2 = loco2;
//                        conducaoCorpoNovo.sStatus_Loc3 = loco3;
//                        conducaoCorpoNovo.sStatus_Loc4 = loco4;
//                        conducaoCorpoNovo.sStatus_Loc5 = loco5;
//                        conducaoCorpoNovo.sStatus_Loc6 = loco6;

//                        if (conducaoCompletaAlterada[0].IdaVolta == "Ida")
//                        {

//                            conducaoCorpoNovo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kminicio) && e.KmFim >= float.Parse(kmFim));
//                        }

//                        else
//                        {

//                            conducaoCorpoNovo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kmFim) && e.KmFim >= float.Parse(kminicio));

//                        }

//                        IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Cabecalho");
//                        conducaoCorpos.Add(conducaoCorpoNovo);
//                        _conducaoCorpoService.Cadastrar(conducaoCorpoNovo);
//                        var quantCabecalhos =
//                            _conducaoCompletaService.Listar(
//                                e =>
//                                    e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao &&
//                                    (e.CStatus.ToString().ToLower() == "e")).Distinct(a);
//                        foreach (var t in quantCabecalhos)
//                        {
//                            var conducaoCompletaNovo = new ConducaoCompleta();
//                            TryUpdateModel(conducaoCompletaNovo, form);
//                            conducaoCompletaNovo.Cabecalho = t.Cabecalho;
//                            conducaoCompletaNovo.Corpo = conducaoCorpoNovo;
//                            conducaoCompletaNovo.CStatus = 'E';
//                            conducaoCompletaNovo.DDataRegistro = DateTime.Now;
//                            conducaoCompletaNovo.Usuario = Usuario;
//                            conducaoCompletaNovo.SRodape = rodape;
//                            conducaoCompletaNovo.IdaVolta = conducaoCompletaAlterada[0].IdaVolta;
//                            conducaoCompletaNovo.DDataAlteracao = DateTime.Now;
//                            _conducaoCompletaService.Cadastrar(conducaoCompletaNovo);
//                        }
//                    }


//                    var verificador = contarQuantidadeCorpo + 1;

//                    contenAlgo = form["km" + verificador] != null ? form["km" + verificador].ToString(CultureInfo.InvariantCulture) : null;

//                    for (int i = 0; i < 6; i++)
//                    {
//                        if (contenAlgo == null)
//                        {
//                            contenAlgo = form["km" + verificador] != null ? form["km" + verificador].ToString() : null;
//                            if (contenAlgo == null)
//                            {
//                                contarQuantidadeCorpo++;
//                            }
//                        }
//                        verificador++;
//                    }
//                    contador++;
//                }
//                conducaoCompletaAlterada = _conducaoCompletaService.Listar(e => e.Cabecalho == cabecalho).ToList();
//                if (conducaoCompletaAlterada.Count > contador)
//                {
//                    var indiceApagar = contador;
//                    for (int i = indiceApagar; i < conducaoCompletaAlterada.Count; i++)
//                    {
//                        var corpos = new List<Conducao_Corpo> { conducaoCompletaAlterada[i].Corpo };
//                        foreach (var t in corpos)
//                        {
//                            var conducaoCompletaApagar = _conducaoCompletaService.Listar(e => e.Corpo == t).ToList();
//                            foreach (var t2 in conducaoCompletaApagar)
//                            {
//                                _conducaoCompletaService.Remover(t2);
//                            }
//                        }
//                    }
//                    for (var i = indiceApagar; i < conducaoCompletaAlterada.Count; i++)
//                    {

//                        _conducaoCorpoService.Remover(conducaoCompletaAlterada[i].Corpo);
//                    }
//                }
//                IList<Combinacao> combinacoes = new List<Combinacao>();
//                var combinacao1 = _combinacaoService.ObterUporId(int.Parse(tipoLotação1));
//                var combinacao2 = _combinacaoService.ObterUporId(int.Parse(tipoLotação2));
//                var combinacao3 = _combinacaoService.ObterUporId(int.Parse(tipoLotação3));
//                var combinacao4 = _combinacaoService.ObterUporId(int.Parse(tipoLotação4));
//                var combinacao5 = _combinacaoService.ObterUporId(int.Parse(tipoLotação5));
//                var combinacao6 = _combinacaoService.ObterUporId(int.Parse(tipoLotação6));
//                if (combinacao1 != null)
//                {
//                    combinacoes.Add(combinacao1);
//                }

//                if (combinacao2 != null)
//                {
//                    combinacoes.Add(combinacao2);
//                }

//                if (combinacao3 != null)
//                {
//                    combinacoes.Add(combinacao3);
//                }
//                if (combinacao4 != null)
//                {
//                    combinacoes.Add(combinacao4);
//                }
//                if (combinacao5 != null)
//                {
//                    combinacoes.Add(combinacao5);
//                }
//                if (combinacao6 != null)
//                {
//                    combinacoes.Add(combinacao6);
//                }
//                var cabecalhoVelhos =
//                    _conducaoCabecalhoService.Listar(
//                        e =>
//                            e.SCodigoConducao == cabecalho.SCodigoConducao &&
//                            e.Versao == conducaoCompletaAlterada[0].Cabecalho.Versao);
//                var combinacoesNovasGravar = new List<Combinacao>();
//                foreach (var t in cabecalhoVelhos)
//                {
//                    if (t.Combinacao != combinacao1 && t.Combinacao != combinacao2 &&
//                        t.Combinacao != combinacao3 && t.Combinacao != combinacao4 &&
//                        t.Combinacao != combinacao5 && t.Combinacao != combinacao6)
//                    {
//                        var objConducaoCompletaExcluir =
//                            _conducaoCompletaService.Listar(
//                                e => e.Cabecalho.Combinacao == t.Combinacao && e.CStatus.ToString().ToLower() == "e")
//                                .ToList();
//                        foreach (var T in objConducaoCompletaExcluir)
//                        {
//                            _conducaoCompletaService.Remover(T);
//                        }
//                        _conducaoCabecalhoService.Remover(t);
//                    }
//                }
//                var numeroparaverificar2 = -1;
//                cabecalhoVelhos =
//                    _conducaoCabecalhoService.Listar(
//                        e =>
//                            e.SCodigoConducao == cabecalho.SCodigoConducao &&
//                            e.Versao == conducaoCompletaAlterada[0].Cabecalho.Versao);
//                foreach (var t in cabecalhoVelhos)
//                {
//                    t.DVigencia = DateTime.Parse(form["DVigencia"]);
//                    if (t.Combinacao == combinacao1)
//                    {
//                        numeroparaverificar2 = 0;
//                    }
//                    else if (t.Combinacao == combinacao2)
//                    {
//                        numeroparaverificar2 = 1;
//                    }
//                    else if (t.Combinacao == combinacao3)
//                    {
//                        numeroparaverificar2 = 2;
//                    }
//                    else if (t.Combinacao == combinacao4)
//                    {
//                        numeroparaverificar2 = 3;
//                    }
//                    else if (t.Combinacao == combinacao5)
//                    {
//                        numeroparaverificar2 = 4;
//                    }
//                    else if (t.Combinacao == combinacao6)
//                    {
//                        numeroparaverificar2 = 5;
//                    }
//                    if (numeroparaverificar2 == 0)
//                    {
//                        if (combinacao1 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial1);
//                            t.FPesoFinal = float.Parse(pesoFinal1);
//                        }

//                    }
//                    else if (numeroparaverificar2 == 1)
//                    {
//                        if (combinacao2 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial2);
//                            t.FPesoFinal = float.Parse(pesoFinal2);
//                        }
//                    }
//                    else if (numeroparaverificar2 == 2)
//                    {
//                        if (combinacao3 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial3);
//                            t.FPesoFinal = float.Parse(pesoFinal3);
//                        }
//                    }
//                    else if (numeroparaverificar2 == 3)
//                    {
//                        if (combinacao4 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial4);
//                            t.FPesoFinal = float.Parse(pesoFinal4);
//                        }
//                    }
//                    else if (numeroparaverificar2 == 4)
//                    {
//                        if (combinacao5 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial5);
//                            t.FPesoFinal = float.Parse(pesoFinal5);
//                        }
//                    }
//                    else if (numeroparaverificar2 == 5)
//                    {
//                        if (combinacao6 == t.Combinacao)
//                        {
//                            t.FPesoIncial = float.Parse(pesoInicial6);
//                            t.FPesoFinal = float.Parse(pesoFinal6);
//                        }
//                    }

//                    _conducaoCabecalhoService.Cadastrar(t);
//                }
//                cabecalhoVelhos = _conducaoCabecalhoService.Listar(
//                                  e =>
//                                      e.SCodigoConducao == cabecalho.SCodigoConducao &&
//                                      e.Versao == conducaoCompletaAlterada[0].Cabecalho.Versao);

//                var numeroparaverificar = -1;
//                IList<ConducaoCompleta> listadeConducoesCompletas;
//                if (cabecalhoVelhos.Count < combinacoes.Count)
//                {
//                    if (cabecalhoVelhos.Count == 0)
//                    {
//                        combinacoesNovasGravar.AddRange(combinacoes);
//                    }
//                    else
//                    {
//                        var nova = new Combinacao();
//                        foreach (Combinacao t1 in combinacoes)
//                        {
//                            foreach (ConducaoCabecalho t in cabecalhoVelhos)
//                            {
//                                if (t1 == t.Combinacao)
//                                {
//                                    nova = null;
//                                    break;
//                                }
//                                else
//                                {
//                                    nova = t1;
//                                }
//                            }
//                            if (nova != null)
//                            {
//                                combinacoesNovasGravar.Add(nova);
//                            }
//                        }
//                    }
//                    foreach (Combinacao t1 in combinacoesNovasGravar)
//                    {
//                        var trecho = _trechoService.ObterUporId((Convert.ToInt32(form["TRECHO"])));
//                        var conducaoCabecalhoNovo = new ConducaoCabecalho();
//                        if (t1 == combinacao1)
//                        {
//                            numeroparaverificar = 0;
//                        }
//                        else if (t1 == combinacao2)
//                        {
//                            numeroparaverificar = 1;
//                        }
//                        else if (t1 == combinacao3)
//                        {
//                            numeroparaverificar = 2;
//                        }
//                        else if (t1 == combinacao4)
//                        {
//                            numeroparaverificar = 3;
//                        }
//                        else if (t1 == combinacao5)
//                        {
//                            numeroparaverificar = 4;
//                        }
//                        else if (t1 == combinacao6)
//                        {
//                            numeroparaverificar = 5;
//                        }
//                        TryUpdateModel(conducaoCabecalhoNovo, form);
//                        conducaoCabecalhoNovo.Trecho = trecho;

//                        switch (numeroparaverificar)
//                        {
//                            case 0:
//                                conducaoCabecalhoNovo.Combinacao = combinacao1;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial1);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal1);
//                                break;
//                            case 1:
//                                conducaoCabecalhoNovo.Combinacao = combinacao2;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial2);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal2);
//                                break;
//                            case 2:
//                                conducaoCabecalhoNovo.Combinacao = combinacao3;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial3);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal3);
//                                break;
//                            case 3:
//                                conducaoCabecalhoNovo.Combinacao = combinacao4;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial4);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal4);
//                                break;
//                            case 4:
//                                conducaoCabecalhoNovo.Combinacao = combinacao5;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial5);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal5);
//                                break;
//                            case 5:
//                                conducaoCabecalhoNovo.Combinacao = combinacao6;
//                                conducaoCabecalhoNovo.FPesoIncial = float.Parse(pesoInicial6);
//                                conducaoCabecalhoNovo.FPesoFinal = float.Parse(pesoFinal6);
//                                break;
//                        }
//                        _conducaoCabecalhoService.Cadastrar(conducaoCabecalhoNovo);
//                        listadeConducoesCompletas = _conducaoCompletaService.Listar(e => e.CStatus.ToString().ToLower() == "e" && e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao);
//                        if (listadeConducoesCompletas.Count == 0)
//                        {
//                            contador = 1;
//                            contenAlgo = form["km" + contador].ToString();
//                            conducaoCorpos = new List<Conducao_Corpo>();
//                            while (contenAlgo != null)
//                            {
//                                var kminicio = form["km" + contador.ToString()].Replace('.', ',');
//                                var loco1 = form["sStatus_Loc1" + contador.ToString()];
//                                var loco2 = form["sStatus_Loc2" + contador.ToString()];
//                                var loco3 = form["sStatus_Loc3" + contador.ToString()];
//                                var loco4 = form["sStatus_Loc4" + contador.ToString()];
//                                var loco5 = form["sStatus_Loc5" + contador.ToString()];
//                                var loco6 = form["sStatus_Loc6" + contador.ToString()];
//                                var descricao = form["descricao" + contador.ToString()];
//                                var kmFim = form["kmfim" + contador.ToString()].Replace('.', ',');
//                                var observacões = form["obs" + contador.ToString()];
//                                var conducaoCorpoNovo = new Conducao_Corpo();
//                                TryUpdateModel(conducaoCorpoNovo, form);
//                                conducaoCorpoNovo.Descricao_Conducao = descricao;
//                                conducaoCorpoNovo.KmInicio = float.Parse(kminicio);
//                                conducaoCorpoNovo.KmFim = float.Parse(kmFim);
//                                conducaoCorpoNovo.sObservacoes = observacões;
//                                conducaoCorpoNovo.sStatus_Loc1 = loco1;
//                                conducaoCorpoNovo.sStatus_Loc2 = loco2;
//                                conducaoCorpoNovo.sStatus_Loc3 = loco3;
//                                conducaoCorpoNovo.sStatus_Loc4 = loco4;
//                                conducaoCorpoNovo.sStatus_Loc5 = loco5;
//                                conducaoCorpoNovo.sStatus_Loc6 = loco6;
//                                if (conducaoCompletaAlterada[0].IdaVolta == "Ida")
//                                {

//                                    conducaoCorpoNovo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kminicio) && e.KmFim >= float.Parse(kmFim));
//                                }

//                                else
//                                {

//                                    conducaoCorpoNovo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kmFim) && e.KmFim >= float.Parse(kminicio));

//                                }


//                                conducaoCorpos.Add(conducaoCorpoNovo);
//                                _conducaoCorpoService.Cadastrar(conducaoCorpoNovo);

//                                contador++;
//                                contenAlgo = form["km" + contador] != null ? form["km" + contador].ToString() : null;
//                            }

//                            foreach (var t in conducaoCorpos)
//                            {
//                                var conducaoCompleta = new ConducaoCompleta();
//                                TryUpdateModel(conducaoCompleta, form);
//                                conducaoCompleta.Cabecalho = conducaoCabecalhoNovo;
//                                conducaoCompleta.Corpo = t;
//                                conducaoCompleta.DDataRegistro = DateTime.Now;
//                                conducaoCompleta.DDataRegistroEspecialista = DateTime.Now;
//                                conducaoCompleta.Usuario = Usuario;
//                                conducaoCompleta.IdaVolta = conducaoCompletaAlterada[0].IdaVolta;
//                                conducaoCompleta.DDataAlteracao = DateTime.Now;
//                                _conducaoCompletaService.Cadastrar(conducaoCompleta);
//                            }

//                        }
//                        else
//                        {
//                            IEqualityComparer<ConducaoCompleta> a = new lambdaComparer<ConducaoCompleta>("Corpo");
//                            var objConducaoCompletaExcluir =
//                                listadeConducoesCompletas.Distinct(a);
//                            foreach (var T in objConducaoCompletaExcluir)
//                            {
//                                var conducaoCompletaNova = new ConducaoCompleta();
//                                TryUpdateModel(conducaoCompletaNova, form);
//                                conducaoCompletaNova.Cabecalho = conducaoCabecalhoNovo;
//                                conducaoCompletaNova.Corpo = T.Corpo;
//                                conducaoCompletaNova.CStatus = 'C';
//                                conducaoCompletaNova.DDataRegistro = DateTime.Now;
//                                conducaoCompletaNova.DDataRegistroEspecialista = DateTime.Now;
//                                conducaoCompletaNova.Usuario = Usuario;
//                                conducaoCompletaNova.IdaVolta = conducaoCompletaAlterada[0].IdaVolta;
//                                conducaoCompletaNova.DDataAlteracao = DateTime.Now;
//                                _conducaoCompletaService.Cadastrar(conducaoCompletaNova);
//                            }
//                            listadeConducoesCompletas = _conducaoCompletaService.Listar(e => true);
//                            var objConducaoCompletaExcluir2 =
//                                listadeConducoesCompletas.Where(
//                                    e =>
//                                        e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao &&
//                                        e.CStatus.ToString().ToLower() == "e").ToList();
//                            foreach (var t in objConducaoCompletaExcluir2)
//                            {

//                                t.CStatus = 'C';
//                                t.DDataRegistro = DateTime.Now;
//                                t.DDataRegistroEspecialista = DateTime.Now;
//                                t.DDataAlteracao = DateTime.Now;

//                                _conducaoCompletaService.Cadastrar(t);
//                            }


//                        }
//                    }
//                }
//                else
//                {
//                    listadeConducoesCompletas = _conducaoCompletaService.Listar(e => true);
//                    var objConducaoCompletaExcluir2 =
//                        _conducaoCompletaService.Listar(
//                            e =>
//                                e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao &&
//                                e.CStatus.ToString().ToLower() == "e").ToList();
//                    foreach (var t in objConducaoCompletaExcluir2)
//                    {

//                        t.CStatus = 'C';
//                        t.DDataRegistro = DateTime.Now;
//                        t.DDataRegistroEspecialista = DateTime.Now;
//                        t.DDataAlteracao = DateTime.Now;

//                        _conducaoCompletaService.Cadastrar(t);
//                    }
//                }
//                var versoaantiga = cabecalho.Versao - 1;
//                var conducaoCompletaAntiga =
//                    _conducaoCompletaService.Listar(
//                        e =>
//                            e.Cabecalho.Versao == versoaantiga &&
//                            e.Cabecalho.SCodigoConducao == cabecalho.SCodigoConducao);
//                foreach (var t in conducaoCompletaAntiga)
//                {
//                    t.CStatus = 'D';
//                    t.DDataAlteracao = DateTime.Now;
//                    _conducaoCompletaService.Cadastrar(t);
//                }
//                var home = new HomeController();
//                home.CalcularPendencias();
//            }

//            else if (conducaoCompletaAlterada[0].CStatus.ToString().ToLower() == "a")
//            {
//                if (form["cbx_Tipodelotacao1"] != null)
//                {
//                    tipoLotação1 = form["cbx_Tipodelotacao1"].Replace('.', ',');
//                    pesoInicial1 = form["iPeso_Incial1"].Replace('.', ',');
//                    pesoFinal1 = form["iPeso_Final1"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                if (form["cbx_Tipodelotacao2"] != null)
//                {
//                    tipoLotação2 = form["cbx_Tipodelotacao2"].ToString();
//                    pesoInicial2 = form["iPeso_Incial2"].Replace('.', ',');
//                    pesoFinal2 = form["iPeso_Final2"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                if (form["cbx_Tipodelotacao3"] != null)
//                {
//                    tipoLotação3 = form["cbx_Tipodelotacao3"].ToString();
//                    pesoInicial3 = form["iPeso_Incial3"].Replace('.', ',');
//                    pesoFinal3 = form["iPeso_Final3"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                if (form["cbx_Tipodelotacao4"] != null)
//                {
//                    tipoLotação4 = form["cbx_Tipodelotacao4"].ToString();
//                    pesoInicial4 = form["iPeso_Incial4"].Replace('.', ',');
//                    pesoFinal4 = form["iPeso_Final4"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                if (form["cbx_Tipodelotacao5"] != null)
//                {
//                    tipoLotação5 = form["cbx_Tipodelotacao5"].ToString();
//                    pesoInicial5 = form["iPeso_Incial5"].Replace('.', ',');
//                    pesoFinal5 = form["iPeso_Final5"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }
//                if (form["cbx_Tipodelotacao6"] != null)
//                {
//                    tipoLotação6 = form["cbx_Tipodelotacao6"].ToString();
//                    pesoInicial6 = form["iPeso_Incial6"].Replace('.', ',');
//                    pesoFinal6 = form["iPeso_Final6"].Replace('.', ',');
//                    quantidadedeTipoLotacao++;
//                }

//                var trecho = _trechoService.ObterUporId(Convert.ToInt32(form["TRECHO"]));
//                var contador = 1;
//                var contenAlgo = form["km" + contador].ToString();
//                while (contenAlgo != null)
//                {

//                    var conducaoCorpo = new Conducao_Corpo();
//                    var kminicio = form["km" + contador.ToString()].Replace('.', ',');
//                    var loco1 = form["sStatus_Loc1" + contador.ToString()];
//                    var loco2 = form["sStatus_Loc2" + contador.ToString()];
//                    var loco3 = form["sStatus_Loc3" + contador.ToString()];
//                    var loco4 = form["sStatus_Loc4" + contador.ToString()];
//                    var loco5 = form["sStatus_Loc5" + contador.ToString()];
//                    var loco6 = form["sStatus_Loc6" + contador.ToString()];
//                    var descricao = form["descricao" + contador.ToString()];
//                    var kmFim = form["kmfim" + contador.ToString()].Replace('.', ',');
//                    var observacões = form["obs" + contador.ToString()];
//                    TryUpdateModel(conducaoCorpo, form);
//                    conducaoCorpo.Descricao_Conducao = descricao;
//                    conducaoCorpo.KmInicio = float.Parse(kminicio);
//                    conducaoCorpo.KmFim = float.Parse(kmFim);
//                    conducaoCorpo.sObservacoes = observacões;
//                    conducaoCorpo.sStatus_Loc1 = loco1;
//                    conducaoCorpo.sStatus_Loc2 = loco2;
//                    conducaoCorpo.sStatus_Loc3 = loco3;
//                    conducaoCorpo.sStatus_Loc4 = loco4;
//                    conducaoCorpo.sStatus_Loc5 = loco5;
//                    conducaoCorpo.sStatus_Loc6 = loco6;
//                    var contadorAuxiliar = contador - 1;
//                    if (conducaoCompletaAlterada[0].IdaVolta == "Ida")
//                    {

//                        conducaoCorpo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kminicio) && e.KmFim >= float.Parse(kmFim));
//                    }

//                    else
//                    {

//                        conducaoCorpo.Sb = _sbService.ObterporId2(e => e.KmInicio <= float.Parse(kmFim) && e.KmFim >= float.Parse(kminicio));

//                    }

//                    conducaoCorpos.Add(conducaoCorpo);
//                    _conducaoCorpoService.Cadastrar(conducaoCorpo);
//                    contador++;
//                    contenAlgo = form["km" + contador] != null ? form["km" + contador].ToString() : null;
//                    for (int i = 0; i < 6; i++)
//                    {
//                        if (contenAlgo == null)
//                        {
//                            contador++;
//                            contenAlgo = form["km" + contador] != null ? form["km" + contador].ToString() : null;
//                        }
//                    }
//                }


//                IList<Combinacao> combinacoes = new List<Combinacao>();
//                var combinacao1 = _combinacaoService.ObterUporId(int.Parse(tipoLotação1));
//                var combinacao2 = _combinacaoService.ObterUporId(int.Parse(tipoLotação2));
//                var combinacao3 = _combinacaoService.ObterUporId(int.Parse(tipoLotação3));
//                var combinacao4 = _combinacaoService.ObterUporId(int.Parse(tipoLotação4));
//                var combinacao5 = _combinacaoService.ObterUporId(int.Parse(tipoLotação5));
//                var combinacao6 = _combinacaoService.ObterUporId(int.Parse(tipoLotação6));
//                if (combinacao1 != null)
//                {
//                    combinacoes.Add(combinacao1);
//                }

//                if (combinacao2 != null)
//                {
//                    combinacoes.Add(combinacao2);
//                }

//                if (combinacao3 != null)
//                {
//                    combinacoes.Add(combinacao3);
//                }
//                if (combinacao4 != null)
//                {
//                    combinacoes.Add(combinacao4);
//                }
//                if (combinacao5 != null)
//                {
//                    combinacoes.Add(combinacao5);
//                }
//                if (combinacao6 != null)
//                {
//                    combinacoes.Add(combinacao6);
//                }


//                foreach (Combinacao t1 in combinacoes)
//                {
//                    var conducaocabecalho = new ConducaoCabecalho();
//                    TryUpdateModel(conducaocabecalho, form);
//                    if (combinacao1 == t1)
//                    {
//                        conducaocabecalho.Combinacao = combinacao1;
//                        conducaocabecalho.FPesoIncial = float.Parse(pesoInicial1);
//                        conducaocabecalho.FPesoFinal = float.Parse(pesoFinal1);
//                    }


//                    else

//                        if (combinacao2 == t1)
//                        {
//                            conducaocabecalho.Combinacao = combinacao2;
//                            conducaocabecalho.FPesoIncial = float.Parse(pesoInicial2);
//                            conducaocabecalho.FPesoFinal = float.Parse(pesoFinal2);
//                        }

//                        else

//                            if (combinacao3 == t1)
//                            {
//                                conducaocabecalho.Combinacao = combinacao3;
//                                conducaocabecalho.FPesoIncial = float.Parse(pesoInicial3);
//                                conducaocabecalho.FPesoFinal = float.Parse(pesoFinal3);
//                            }

//                            else

//                                if (combinacao4 == t1)
//                                {
//                                    conducaocabecalho.Combinacao = combinacao4;
//                                    conducaocabecalho.FPesoIncial = float.Parse(pesoInicial4);
//                                    conducaocabecalho.FPesoFinal = float.Parse(pesoFinal4);
//                                }

//                                else

//                                    if (combinacao5 == t1)
//                                    {
//                                        conducaocabecalho.Combinacao = combinacao5;
//                                        conducaocabecalho.FPesoIncial = float.Parse(pesoInicial5);
//                                        conducaocabecalho.FPesoFinal = float.Parse(pesoFinal5);
//                                    }

//                                    else

//                                        if (combinacao6 == t1)
//                                        {
//                                            conducaocabecalho.Combinacao = combinacao6;
//                                            conducaocabecalho.FPesoIncial = float.Parse(pesoInicial6);
//                                            conducaocabecalho.FPesoFinal = float.Parse(pesoFinal6);
//                                        }
//                    conducaocabecalho.Trecho = trecho;
//                    _conducaoCabecalhoService.Cadastrar(conducaocabecalho);
//                    conducaoCabecalhos.Add(conducaocabecalho);

//                    if (Usuario.Cargo.Descricao.ToString().ToLower() == "especialista")
//                    {
//                        foreach (var t in conducaoCorpos)
//                        {
//                            var conducaoCompleta = new ConducaoCompleta();
//                            TryUpdateModel(conducaoCompleta, form);
//                            conducaoCompleta.Cabecalho = conducaocabecalho;
//                            conducaoCompleta.Corpo = t;
//                            conducaoCompleta.DDataRegistro = DateTime.Now;
//                            conducaoCompleta.DDataRegistroEspecialista = DateTime.Now;
//                            conducaoCompleta.Usuario = Usuario;
//                            conducaoCompleta.DDataAlteracao = DateTime.Now;
//                            conducaoCompleta.CStatus = 'C';
//                            conducaoCompleta.IdaVolta = conducaoCompletaAlterada[0].IdaVolta;
//                            _conducaoCompletaService.Cadastrar(conducaoCompleta);
//                        }
//                    }
//                    else
//                    {
//                        foreach (var t in conducaoCorpos)
//                        {
//                            var conducaoCompleta = new ConducaoCompleta();
//                            TryUpdateModel(conducaoCompleta, form);
//                            conducaoCompleta.Cabecalho = conducaocabecalho;
//                            conducaoCompleta.Corpo = t;
//                            conducaoCompleta.DDataRegistro = DateTime.Now;
//                            conducaoCompleta.DDataRegistroEspecialista = DateTime.Now;
//                            conducaoCompleta.Usuario = Usuario;
//                            conducaoCompleta.CStatus = 'E';
//                            conducaoCompleta.DDataAlteracao = DateTime.Now;
//                            conducaoCompleta.IdaVolta = conducaoCompletaAlterada[0].IdaVolta;
//                            _conducaoCompletaService.Cadastrar(conducaoCompleta);
//                        }
//                    }
//                }

//                var versoaantiga = conducaoCabecalhos[0].Versao - 1;
//                var conducaoCompletaAntiga =
//                    _conducaoCompletaService.Listar(
//                        e =>
//                            e.Cabecalho.Versao == versoaantiga &&
//                            e.Cabecalho.SCodigoConducao == conducaoCabecalhos[0].SCodigoConducao);
//                foreach (var t in conducaoCompletaAntiga)
//                {
//                    t.CStatus = 'D';
//                    t.DDataAlteracao = DateTime.Now;
//                    _conducaoCompletaService.Cadastrar(t);
//                }
//            }
//            return RedirectToAction("Listar");
//        }

//        public ActionResult ValidarPadrao(int id)
//        {
//            var bolsa = new ConjuntosListasEObj2();
//            var conducaoCorpo = new Dominios.Entidades.Conducao_Corpo();
//            //int paginaatual = page.HasValue ? page.Value : 1;



//            IList<ConducaoCompleta> condaucaoCompleta = _conducaoCompletaService.Listar(e => e.Cabecalho.Id == id);
//            ConducaoCabecalho conducaoCabecalho = _conducaoCabecalhoService.ObterUporId(id);
//            bolsa.Trechos = _trechoService.Listar(e => e.Up.sNome_Up == conducaoCabecalho.Trecho.Up.sNome_Up);
//            bolsa.Conducao_Cabecalho = conducaoCabecalho;
//            bolsa.Conducao_Completas = conducaoCompleta;
//            bolsa.Conducao_Completa = _conducaoCompletaService.ObterporId2(e => e.Cabecalho.Id == id);
//            bolsa.EnumerableConducao_Cabecalhos = _conducaoCabecalhoService.Listar(e => e.SCodigoConducao == bolsa.Conducao_Cabecalho.SCodigoConducao);
//            return View(bolsa);
//        }

//        public ActionResult Detalhes(int id)
//        {


//            return View();
//        }





//    }
//}
