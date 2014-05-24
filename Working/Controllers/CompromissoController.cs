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
using Working.ViewsModels;

namespace Working.Controllers
{
    public class CompromissoController : Controller
    {
        private readonly CompromissoService _compromissoService;

        public CompromissoController()
        {
            _compromissoService = Dependencia.Resolver<CompromissoService>();
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

        public JsonResult CadastrarAjax(string data, string hora, string descricao)
        {
            var compromisso = new Compromisso();
            compromisso.Data = Convert.ToDateTime(data);
            compromisso.Hora = hora;
            compromisso.Descricao = descricao;
            compromisso.DataRegistro = DateTime.Now;
            _compromissoService.Cadastrar(compromisso);
            return null;
        }

        public void DeletaCompromisso(int id)
        {
            var compromisso = _compromissoService.ObterPorId(id);
            _compromissoService.Remover(compromisso);
        }

        public JsonResult AlterarAjax(int id, string data, string hora, string descricao)
        {
            var compromisso = _compromissoService.ObterPorId(id);
            compromisso.Data = Convert.ToDateTime(data);
            compromisso.Hora = hora;
            compromisso.Descricao = descricao;
            compromisso.DataRegistro = DateTime.Now;
            _compromissoService.Cadastrar(compromisso);
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
            _compromissoService.Cadastrar(compromisso);
            return null;
        }
    }
}
