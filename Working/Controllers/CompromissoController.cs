using System;
using System.CodeDom;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class CompromissoController : Controller
    {
        private readonly CompromissoService _compromissoService;
        private readonly FuncionarioService _funcionarioService;
        private readonly JobService _jobService;

        public CompromissoController()
        {
            _compromissoService = Dependencia.Resolver<CompromissoService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _jobService = Dependencia.Resolver<JobService>();
        }

        public ActionResult Index()
        {
            var lista = _compromissoService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Antigos()
        {
            var lista = _compromissoService.Listar(e => true).OrderBy(e=>e.Data).ThenBy(e=>e.Hora);
            return View(lista);
        }

        public JsonResult CadastrarAjax(string data, string hora, string descricao, string idJob)
        {
            var compromisso = new Compromisso();
            compromisso.Data = Convert.ToDateTime(data);
            compromisso.Hora = hora;
            compromisso.Descricao = descricao;
            compromisso.DataRegistro = DateTime.Now;
            compromisso.UltimoFuncionario = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
            if (idJob != null)
            {
                compromisso.Job = _jobService.ObterPorId(Convert.ToInt16(idJob));
            }
            _compromissoService.Cadastrar(compromisso);
            return null;
        }

        public void DeletaCompromisso(int id)
        {
            var compromisso = _compromissoService.ObterPorId(id);
            _compromissoService.Remover(compromisso);
        }

        public JsonResult AlterarAjax(int id, string data, string hora, string descricao, string idJob)
        {
            var funcionario =
                _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);

            if (idJob != null)
            {
                var listaCompromisso =
                    _compromissoService.Listar(e => e.Job == _jobService.ObterPorId(Convert.ToInt16(idJob)));

                foreach (var i in listaCompromisso)
                {
                    i.Data = Convert.ToDateTime(data);
                    i.UltimoFuncionario = funcionario;
                    i.Descricao = descricao;
                    i.Hora = hora;
                    i.DataRegistro = DateTime.Now;
                    _compromissoService.Cadastrar(i);
                }
            }
            else
            {
                var compromisso = _compromissoService.ObterPorId(id);

                compromisso.Data = Convert.ToDateTime(data);
                compromisso.Hora = hora;
                compromisso.Descricao = descricao;
                compromisso.DataRegistro = DateTime.Now;
                compromisso.UltimoFuncionario = funcionario;
                _compromissoService.Cadastrar(compromisso);
            }
            return null;
        }

        public JsonResult PopularModal(int id)
        {
            var compromisso = _compromissoService.ObterPorId(id);
            Lista lista = new Lista();
            lista.data = compromisso.Data.Day.ToString("D2") + "/" + compromisso.Data.ToString("MM") + "/" + compromisso.Data.Year;
            lista.hora = compromisso.Hora;
            lista.conteudo = compromisso.Descricao;
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmarCompromisso(int id)
        {
            var compromisso = _compromissoService.ObterPorId(id);
            compromisso.Situacao = 1;
            compromisso.UltimoFuncionario = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
            compromisso.DataRegistro = DateTime.Now;
            _compromissoService.Cadastrar(compromisso);
            return null;
        }

        public JsonResult ListarCompromissosJson()
        {
            var compromisso =
                _compromissoService.Listar(e => true)
                    .OrderBy(e=>e.Data)
                    .ThenBy(e => e.Hora)
                    .Where(e => e.Situacao == 0 && e.Data>=DateTime.Now.AddDays(-1))
                    .Take(5);

            var lista = new List<Objeto>();
            foreach (var i in compromisso)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    Data = i.Data.Day.ToString("D2") + "/" + i.Data.ToString("MMM"),
                    Hora = i.Hora,
                    Descricao = i.Descricao,
                    Alterado = _funcionarioService.ObterPorId(i.UltimoFuncionario.Id).Nome
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
